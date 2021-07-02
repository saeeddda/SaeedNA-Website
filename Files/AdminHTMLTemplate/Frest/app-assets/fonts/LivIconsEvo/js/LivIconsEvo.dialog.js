jQuery(document).ready(function($) {

	"use strict";

	var	values = {afterAdd:false, afterUpdate:false, afterErase:false, drawEase:'Power1.easeOut', eraseEase:'Power1.easeOut', addID:'', addClass:'', addCss:'', addLink:'', linkTarget:''},
		d_o = LivIconsEvoDefaults(),
		first_init = undefined,
		rendered = false,
		html_code_full,
		html_code_full_collapsed,
		html_code_compact,
		html_code_compact_collapsed,
		js_code_full,
		js_code_full_collapsed,
		js_code_compact,
		js_code_compact_collapsed,
		short_code_full,
		short_code_compact,
		clipboard = false;

	$('body').on('click', '#lievo-add-button', function() {
		if (first_init !== false) {
			first_init = true;
		}
		$('#lievo-dialog-overlay, #lievo-dialog-wrap').show();
		equalHeight( '#lievo-icons', '#lievo-dialog-wrap', 115 );
		equalHeight( '#lievo-options', '#lievo-dialog-wrap', 150 );
		var t = setTimeout(
			function() {
				$('.lievo-hidden').addLiviconEvo({afterAdd:function () {$('.lievo-hidden').remove()}});
				if (!rendered) {
					$('.livicon-evo-in-list').addLiviconEvo();
					rendered = true;
				};
				$('body').addClass('lievo-modal-open');
				clearTimeout(t);
			}, 50
		);
	});
	
	$('#lievo-dialog form').each(function() {
		this.reset();
	});

	$(document).ajaxStop(function() {
		if (first_init) {
			$('.lievo-loader').animate({opacity: 0}, 400).delay(400).hide();
			$('.lievo-icon-loading').delay(400).remove();
			$('#lievo-dialog').css('display', 'block');
			$('#lievo-dialog').animate({opacity: 1}, 600);
		};
	});

	$('#lievo-icons > div').hide();
	$('#lievo-icons > div').first().show();
	$('#lievo-category li').each(function () {
		$(this).on('click', function () {
			var that = $(this),
				key = that.data('key');
			that.siblings().removeClass('lievo-active');
			that.addClass('lievo-active');
			$('.lievo-'+ key +'-icons').siblings().hide();
			$('.lievo-'+ key +'-icons').show();
		});
	});

	//testing bg color for "parent" elem
	$("#lievo-bg-color").spectrum({
		preferredFormat: "hex",
		showInput: true,
		allowEmpty: false,
		showInitial: true,
		color: '#ffffff'
	});
	$('#lievo-bg-color').on('change', function () {
		var color = $(this).val();
		$('#lievo-parent').css('background-color', color);
	});

	//inserting image url
	$('#lievo-morph-image-browse').click(function() {
		var galery = wp.media({ 
			title: 'Insert image URL',
			library: {type: 'image'},
			button: {text: 'Insert'},
			multiple: false
		});
		galery.on('select', function(){
			var user_selection = galery.state().get('selection').first().toJSON();
			$('#opt-morphImage-value').val(user_selection.url);
			$('#opt-morphImage').prop('checked', true).change();
		});
		galery.open();
	});

	//show-hide border of current icon
	$('#lievo_show_border').on('change', function () {
		if ($('#lievo_show_border').is(':checked')) {
			$('#lievo-curicon').css('border-color', '#ccc');
		} else {
			$('#lievo-curicon').css('border-color', 'transparent');
		};
	});

	//close dialog button
	$('#lievo-dialog-close-btn, #lievo-dialog-overlay').on('click', function() {
		$('body').removeClass('lievo-modal-open');
		$('#lievo-dialog-overlay, #lievo-dialog-wrap').hide();
		//for prevent wrong view of a toolbox of the editor
		var t = setTimeout(
			function() {
				$(window).scroll();
				clearTimeout(t);
			}, 50
		);
	});

	//popovers init
	$('[data-toggle="popover"]').popover();

	//open chosen icon settings
	$('.lievo-icon-wrapper').on('click', function () {
		$(this).find('.livicon-evo-in-list').stopLiviconEvo();
		var icon_name = $(this).data('name');
		if ( ! $('#lievo-curicon').liviconEvoOptions() ) {
			$('#lievo-curicon').empty().updateLiviconEvo({
				name: icon_name,
				duration: 'default',
				repeat: 'default',
				repeatDelay: 'default',
				afterAdd: function () {
					var options = $('#lievo-curicon').liviconEvoOptions();
					$('#opt-strokeColor').spectrum('set', options.strokeColor);
					$('#opt-strokeColorAction').spectrum('set', options.strokeColorAction);
					$('#opt-strokeColorAlt').spectrum('set', options.strokeColorAlt);
					$('#opt-strokeColorAltAction').spectrum('set', options.strokeColorAltAction);
					$('#opt-fillColor').spectrum('set', options.fillColor);
					$('#opt-fillColorAction').spectrum('set', options.fillColorAction);
					$('#opt-solidColor').spectrum('set', options.solidColor);
					$('#opt-solidColorAction').spectrum('set', options.solidColorAction);
					$('#opt-solidColorBg').spectrum('set', options.solidColorBg);
					$('#opt-solidColorBgAction').spectrum('set', options.solidColorBgAction);
					$('#def-duration').text(' ('+ options.def_duration +' sec)');
					$('#opt-duration-value').val(options.def_duration);
					$('#def-repeat').text(' ('+ options.def_repeat +')');
					$('#opt-repeat-value').val(options.def_repeat);
					$('#def-repeatDelay').text(' ('+ options.def_repeatDelay +' sec)');
					$('#opt-repeatDelay-value').val(options.def_repeatDelay);
					if (first_init) {
						first_init = false;
						$('input[name=opt-keepStrokeWidthOnResize]').change();
					};
				}
			});
		} else {
			$('#lievo-curicon').empty().updateLiviconEvo({
				name: icon_name,
				duration: 'default',
				repeat: 'default',
				repeatDelay: 'default',
				afterUpdate: function () {
					var options = $('#lievo-curicon').liviconEvoOptions();
					$('#def-duration').text(' ('+ options.def_duration +' sec)');
					$('#opt-duration-value').val(options.def_duration);
					$('#def-repeat').text(' ('+ options.def_repeat +')');
					$('#opt-repeat-value').val(options.def_repeat);
					$('#def-repeatDelay').text(' ('+ options.def_repeatDelay +' sec)');
					$('#opt-repeatDelay-value').val(options.def_repeatDelay);
					$('input[name=opt-duration][value=default]').prop('checked', true).change();
					$('input[name=opt-repeat][value=default]').prop('checked', true).change();
					$('input[name=opt-repeatDelay][value=default]').prop('checked', true).change();
				},
				afterAdd: false
			});
		};
		$('#opt-name').text(icon_name + '.svg');
		$('#lievo-all-icons').hide();
		$('#lievo-curicon-wrapper').show();
	});

	//Resetting function
	function confirmReset() {
		return confirm('Are you sure you want to reset the options?');
	}
	$('#lievo-reset').on('click', function () {
		if ( confirmReset() ) {
			values = {afterAdd:false, afterUpdate:false, afterErase:false, drawEase:'Power1.easeOut', eraseEase:'Power1.easeOut', addID:'', addClass:'', addCss:'', addLink:'', linkTarget:''};
			first_init = true;
			html_code_full ='';
			html_code_full_collapsed ='';
			html_code_compact ='';
			html_code_compact_collapsed ='';
			js_code_full ='';
			js_code_full_collapsed ='';
			js_code_compact ='';
			js_code_compact_collapsed ='';
			short_code_full ='';
			short_code_compact ='';
			clipboard = false;
			
			$('#lievo-dialog form').each(function() {
				this.reset();
			});

			var name = $('#lievo-curicon').liviconEvoOptions().name;
			$('#lievo-curicon').removeLiviconEvo();
			$('.lievo-icon-wrapper[data-name="'+ name.replace(/\.svg/, '') +'"]').trigger('click');
		};
	});

	//go back to icons list
	$('#lievo-back').on('click', function () {
		$('#lievo-all-icons').show();
		$('#lievo-curicon-wrapper').hide();
	});

	//changing tabs
	$(".lievo-tabs-menu li").click(function(event) {
		event.preventDefault();
		$(this).addClass("lievo-current");
		$(this).siblings().removeClass("lievo-current");
		var tab = $(this).data("link");
		$(".lievo-tab-content").not(tab).css("display", "none");
		$(tab).fadeIn();
	});

	//change checked options on focus
	$('#opt-strokeWidth-value').on('focus', function () {
		$('#opt-strokeWidth').prop('checked', true);
	});
	$('#opt-size-value-px').on('focus', function () {
		$('#opt-size-px').prop('checked', true);
	});
	$('#opt-size-value-prc').on('focus', function () {
		$('#opt-size-prc').prop('checked', true);
	});
	$('#opt-customRotate').on('focus', function () {
		$('#opt-rotate').prop('checked', true);
	});
	$('#opt-colorsOnHover-hue').on('focus', function () {
		$('#opt-colorsOnHover').prop('checked', true);
	});
	$('#opt-colorsWhenMorph-hue').on('focus', function () {
		$('#opt-colorsWhenMorph').prop('checked', true);
	});
	$('#opt-morphImage-value').on('focus', function () {
		$('#opt-morphImage').prop('checked', true);
	});
	$('#opt-strokeWidthFactorOnHover-value').on('focus', function () {
		$('#opt-strokeWidthFactorOnHover').prop('checked', true);
	});
	$('#opt-eventOn-value').on('focus', function () {
		$('#opt-eventOn').prop('checked', true);
	});
	$('#opt-duration-value').on('focus', function () {
		$('#opt-duration').prop('checked', true);
	});
	$('#opt-repeat-value').on('focus', function () {
		$('#opt-repeat').prop('checked', true);
	});
	$('#opt-repeatDelay-value').on('focus', function () {
		$('#opt-repeatDelay').prop('checked', true);
	});
	$('input[name=opt-drawColor] + .wp-picker-container').on('click', function () {
		$('#opt-drawColor').prop('checked', true);
	});

	/////  options  /////
	
	//style
	$('input[name=opt-style]').on('change', function () {
		values.style = $(this).val();
		update();
	});

	//size
	$('input[name=opt-size]').on('change', function () {
		values.size = $(this).val();
		if (values.size === 'custom_px') {
			values.size = $('#opt-size-value-px').val() + 'px';
		} else if (values.size === 'custom_prc') {
			values.size = $('#opt-size-value-prc').val() + '%';
		};
		update();
	});
	$('#opt-size-value-px').on('change focus', function () {
		values.size = $('#opt-size-value-px').val() + 'px';
		update();
	});
	$('#opt-size-value-prc').on('change focus', function () {
		values.size = $('#opt-size-value-prc').val() + '%';
		update();
	});

	//strokeStyle
	$('input[name=opt-strokeStyle]').on('change', function () {
		values.strokeStyle = $(this).val();
		update();
	});

	//strokeWidth
	$('input[name=opt-strokeWidth]').on('change', function () {
		values.strokeWidth = $(this).val();
		if (values.strokeWidth === 'custom') {
			values.strokeWidth = $('#opt-strokeWidth-value').val();
		};
		update();
	});
	$('#opt-strokeWidth-value').on('change focus', function () {
		values.strokeWidth = $('#opt-strokeWidth-value').val();
		update();
	});

	//tryToSharpen
	$('input[name=opt-tryToSharpen]').on('change', function () {
		values.tryToSharpen = $(this).val();
		update();
	});

	//rotate
	$('input[name=opt-rotate]').on('change', function () {
		values.rotate = $(this).val();
		if (values.rotate === 'custom') {
			values.rotate = $('#opt-customRotate').val();
		};
		if (values.rotate === '0') {values.rotate = 'none'};
		update();
	});
	$('#opt-customRotate').on('change focus', function () {
		values.rotate = $('#opt-customRotate').val();
		if (values.rotate === '0') {values.rotate = 'none'};
		update();
	});

	//flipHorizontal
	$('input[name=opt-flipHorizontal]').on('change', function () {
		values.flipHorizontal = $(this).val();
		update();
	});

	//flipVertical
	$('input[name=opt-flipVertical]').on('change', function () {
		values.flipVertical = $(this).val();
		update();
	});

	//Colors
	$("#opt-strokeColor, #opt-strokeColorAction, #opt-strokeColorAlt, #opt-strokeColorAltAction, #opt-fillColor, #opt-fillColorAction, #opt-solidColor, #opt-solidColorAction, #opt-solidColorBg, #opt-solidColorBgAction, #opt-drawColor-value").spectrum({
		preferredFormat: "hex",
		showInput: true,
		allowEmpty: false,
		showInitial: true
	});

	$('#opt-strokeColor').on('change', function () {
		values.strokeColor = $(this).val();
		update();
	});
	$('#opt-strokeColorAction').on('change', function () {
		values.strokeColorAction = $(this).val();
		update();
	});
	$('#opt-strokeColorAlt').on('change', function () {
		values.strokeColorAlt = $(this).val();
		update();
	});
	$('#opt-strokeColorAltAction').on('change', function () {
		values.strokeColorAltAction = $(this).val();
		update();
	});
	$('#opt-fillColor').on('change', function () {
		values.fillColor = $(this).val();
		update();
	});
	$('#opt-fillColorAction').on('change', function () {
		values.fillColorAction = $(this).val();
		update();
	});
	$('#opt-solidColor').on('change', function () {
		values.solidColor = $(this).val();
		update();
	});
	$('#opt-solidColorAction').on('change', function () {
		values.solidColorAction = $(this).val();
		update();
	});
	$('#opt-solidColorBg').on('change', function () {
		values.solidColorBg = $(this).val();
		update();
	});
	$('#opt-solidColorBgAction').on('change', function () {
		values.solidColorBgAction = $(this).val();
		update();
	});

	//colors on hover
	$('input[name=opt-colorsOnHover]').on('change', function () {
		values.colorsOnHover = $(this).val();
		if (values.colorsOnHover === 'hue') {
			values.colorsOnHover = 'hue' + $('#opt-colorsOnHover-hue').val();
		};
		update();
	});
	$('#opt-colorsOnHover-hue').on('change focus', function () {
		values.colorsOnHover = 'hue' + $('#opt-colorsOnHover-hue').val();
		update();
	});

	//colorsHoverTime
	$('#opt-colorsHoverTime').on('change', function () {
		values.colorsHoverTime = $(this).val();
		update();
	});

	//colors when morph
	$('input[name=opt-colorsWhenMorph]').on('change', function () {
		values.colorsWhenMorph = $(this).val();
		if (values.colorsWhenMorph === 'hue') {
			values.colorsWhenMorph = 'hue' + $('#opt-colorsWhenMorph-hue').val();
		};
		update();
	});
	$('#opt-colorsWhenMorph-hue').on('change focus', function () {
		values.colorsWhenMorph = 'hue' + $('#opt-colorsWhenMorph-hue').val();
		update();
	});

	//brightness and saturation
	$('#opt-brightness').on('change', function () {
		values.brightness = $(this).val();
		update();
	});
	$('#opt-saturation').on('change', function () {
		values.saturation = $(this).val();
		update();
	});

	//morphState
	$('input[name=opt-morphState]').on('change', function () {
		values.morphState = $(this).val();
		update();
	});

	//morphImage
	$('input[name=opt-morphImage]').on('change', function () {
		values.morphImage = $(this).val();
		if (values.morphImage === 'url') {
			values.morphImage = $('#opt-morphImage-value').val();
		};
		if ( $.trim(values.morphImage) === '') {values.morphImage = 'none'};
		update();
	});
	$('#opt-morphImage-value').on('change focus', function () {
		values.morphImage = $('#opt-morphImage-value').val();
		if ( $.trim(values.morphImage) === '') {values.morphImage = 'none'};
		update();
	});

	//allowMorphImageTransform
	$('input[name=opt-allowMorphImageTransform]').on('change', function () {
		values.allowMorphImageTransform = $(this).val();
		update();
	});

	//strokeWidthFactorOnHover
	$('input[name=opt-strokeWidthFactorOnHover]').on('change', function () {
		values.strokeWidthFactorOnHover = $(this).val();
		if (values.strokeWidthFactorOnHover === 'custom') {
			values.strokeWidthFactorOnHover = $('#opt-strokeWidthFactorOnHover-value').val();
		};
		update();
	});
	$('#opt-strokeWidthFactorOnHover-value').on('change focus', function () {
		values.strokeWidthFactorOnHover = $('#opt-strokeWidthFactorOnHover-value').val();
		update();
	});

	//strokeWidthOnHoverTime
	$('#opt-strokeWidthOnHoverTime').on('change', function () {
		values.strokeWidthOnHoverTime = $(this).val();
		update();
	});

	//keepStrokeWidthOnResize
	$('input[name=opt-keepStrokeWidthOnResize]').on('change', function () {
		values.keepStrokeWidthOnResize = $(this).val();
		update();
	});

	//animated
	$('input[name=opt-animated]').on('change', function () {
		values.animated = $(this).val();
		update();
	});

	//eventType
	$('input[name=opt-eventType]').on('change', function () {
		values.eventType = $(this).val();
		update();
	});

	//eventOn
	$('input[name=opt-eventOn]').on('change', function () {
		values.eventOn = $(this).val();
		if (values.eventOn === 'custom') {
			values.eventOn = $('#opt-eventOn-value').val();
		};
		if ( $.trim(values.eventOn) === '') {values.eventOn = 'self'};
		update();
	});
	$('#opt-eventOn-value').on('change focus', function () {
		values.eventOn = $('#opt-eventOn-value').val();
		if ( $.trim(values.eventOn) === '') {values.eventOn = 'self'};
		update();
	});

	//autoPlay
	$('input[name=opt-autoPlay]').on('change', function () {
		values.autoPlay = $(this).val();
		update();
	});

	//delay
	$('#opt-delay').on('change', function () {
		values.delay = $(this).val();
		update();
	});

	//duration
	$('input[name=opt-duration]').on('change', function () {
		values.duration = $(this).val();
		if (values.duration === 'custom') {
			values.duration = $('#opt-duration-value').val();
		};
		update();
	});
	$('#opt-duration-value').on('change focus', function () {
		values.duration = $('#opt-duration-value').val();
		update();
	});

	//repeat
	$('input[name=opt-repeat]').on('change', function () {
		values.repeat = $(this).val();
		if (values.repeat === 'custom') {
			values.repeat = $('#opt-repeat-value').val();
		};
		update();
	});
	$('#opt-repeat-value').on('change focus', function () {
		values.repeat = $('#opt-repeat-value').val();
		update();
	});

	//repeatDelay
	$('input[name=opt-repeatDelay]').on('change', function () {
		values.repeatDelay = $(this).val();
		if (values.repeatDelay === 'custom') {
			values.repeatDelay = $('#opt-repeatDelay-value').val();
		};
		update();
	});
	$('#opt-repeatDelay-value').on('change focus', function () {
		values.repeatDelay = $('#opt-repeatDelay-value').val();
		update();
	});

	//drawOnViewport
	$('input[name=opt-drawOnViewport]').on('change', function () {
		values.drawOnViewport = $(this).val();
		update();
	});

	//viewportShift
	$('input[name=opt-viewportShift]').on('change', function () {
		values.viewportShift = $(this).val();
		update();
	});

	//drawDelay
	$('#opt-drawDelay').on('change', function () {
		values.drawDelay = $(this).val();
		update();
	});

	//drawTime
	$('#opt-drawTime').on('change', function () {
		values.drawTime = $(this).val();
		update();
	});

	//drawStagger
	$('#opt-drawStagger').on('change', function () {
		values.drawStagger = $(this).val();
		update();
	});

	//drawStartPoint
	$('input[name=opt-drawStartPoint]').on('change', function () {
		values.drawStartPoint = $(this).val();
		update();
	});

	//drawColor
	$('input[name=opt-drawColor]').on('change', function () {
		values.drawColor = $(this).val();
		if (values.drawColor === 'custom') {
			values.drawColor = $('#opt-drawColor-value').val();
		};
		update();
	});
	$('#opt-drawColor-value').on('change', function () {
		values.drawColor = $('#opt-drawColor-value').val();
		update();
	});
	$('#opt-drawColor-value + .sp-replacer').on('click focus', function () {
		$('#opt-drawColor').prop('checked', true).change();
	});

	//drawColorTime
	$('#opt-drawColorTime').on('change', function () {
		values.drawColorTime = $(this).val();
		update();
	});

	//drawReversed
	$('input[name=opt-drawReversed]').on('change', function () {
		values.drawReversed = $(this).val();
		update();
	});

	//drawEase
	$('#opt-drawEase').on('change', function () {
		values.drawEase = $.trim( $(this).val() );
		if (values.drawEase === '') {values.drawEase = 'Power1.easeOut'};
		update();
	});

	//eraseDelay
	$('#opt-eraseDelay').on('change', function () {
		values.eraseDelay = $(this).val();
		update();
	});

	//eraseTime
	$('#opt-eraseTime').on('change', function () {
		values.eraseTime = $(this).val();
		update();
	});

	//eraseStagger
	$('#opt-eraseStagger').on('change', function () {
		values.eraseStagger = $(this).val();
		update();
	});

	//eraseStartPoint
	$('input[name=opt-eraseStartPoint]').on('change', function () {
		values.eraseStartPoint = $(this).val();
		update();
	});

	//eraseReversed
	$('input[name=opt-eraseReversed]').on('change', function () {
		values.eraseReversed = $(this).val();
		update();
	});

	//eraseEase
	$('#opt-eraseEase').on('change', function () {
		values.eraseEase = $.trim( $(this).val() );
		if (values.eraseEase === '') {values.eraseEase = 'Power1.easeOut'};
		update();
	});

	//touchEvents
	$('input[name=opt-touchEvents]').on('change', function () {
		values.touchEvents = $(this).val();
		update();
	});

	//addID
	$('#opt-add-id').on('change', function () {
		values.addID = $.trim( $(this).val() );
		update();
	});

	//addClass
	$('#opt-add-class').on('change', function () {
		values.addClass = $.trim( $(this).val() );
		update();
	});

	//addCss
	$('#opt-add-css').on('change', function () {
		values.addCss = $.trim( $(this).val() );
		update();
	});

	//addLink
	$('#opt-add-link').on('change', function () {
		values.addLink = $.trim( $(this).val() );
		update();
	});

	//linkTarget
	$('#opt-link-target').on('change', function () {
		if( $(this).is(':checked') ) {
			values.linkTarget = 'blank';
		} else {
			values.linkTarget = '';
		}
		update();
	});

	//// logic ////

	//close modal
	$('#lievo-modal-close-btn').on('click', function () {
		$('#lievo-modal').hide();
		$('label[for=collapsed_code]').hide();
	});

	//get Shortcode
	$('#lievo-get-shortcode').on('click', function () {
		var result_code;
		if ( $('#compact_code').is(':checked') ) {
			result_code = short_code_compact;
		} else {
			result_code = short_code_full;
		};
		$('#lievo-code').removeClass().addClass('golang').empty().text(result_code);
		$('.lievo-modal-title').empty().text('WordPress shortcode');
		$('.lievo-modal-note').empty().text('In case you need to quick copy of a shortcode.');
		clipboardAction();
		$('label[for=collapsed_code]').hide();
		$('#lievo-modal').show();
	});

	//get HTML
	$('#lievo-get-html').on('click', function () {
		var result_code;
		if ( $('#collapsed_code').is(':checked') ) {
			if ( $('#compact_code').is(':checked') ) {
				result_code = html_code_compact_collapsed;
			} else {
				result_code = html_code_full_collapsed;
			};
		} else {
			if ( $('#compact_code').is(':checked') ) {
				result_code = html_code_compact;
			} else {
				result_code = html_code_full;
			};
		};
		$('#lievo-code').removeClass().addClass('html').empty().text(result_code);
		$('.lievo-modal-title').empty().text('HTML code');
		$('.lievo-modal-note').empty().text('Use this HTML code to place LivIcon where shortcodes are not supported. For example, into a slider or into another shortcode.');
		clipboardAction();
		$('label[for=collapsed_code]').show();
		$('#lievo-modal').show();
	});

	//get JavaScript
	$('#lievo-get-javascript').on('click', function () {
		var result_code;
		if ( $('#collapsed_code').is(':checked') ) {
			if ( $('#compact_code').is(':checked') ) {
				result_code = js_code_compact_collapsed;
			} else {
				result_code = js_code_full_collapsed;
			};
		} else {
			if ( $('#compact_code').is(':checked') ) {
				result_code = js_code_compact;
			} else {
				result_code = js_code_full;
			};
		};
		$('#lievo-code').removeClass().addClass('javascript').empty().text(result_code);
		$('.lievo-modal-title').empty().text('JavaScript code');
		$('.lievo-modal-note').empty().text('Use this JavaScript code if you want to use LivIcons via programmatic API. Please pay attention that this code ignores "Add ID", "Add Class", "Add Inline Styles" and "Add link" settings.');
		clipboardAction();
		$('label[for=collapsed_code]').show();
		$('#lievo-modal').show();
	});

	//collapsed and compact code
	$('#compact_code').on('change', function () {
		if ( $('#lievo-code').hasClass('html') ) {
			$('#lievo-get-html').trigger('click');
		} else if ( $('#lievo-code').hasClass('javascript') ) {
			$('#lievo-get-javascript').trigger('click');
		} else if ( $('#lievo-code').hasClass('golang') ) {
			$('#lievo-get-shortcode').trigger('click');
		};
	});
	$('#collapsed_code').on('change', function () {
		if ( $('#lievo-code').hasClass('html') ) {
			$('#lievo-get-html').trigger('click');
		} else if ( $('#lievo-code').hasClass('javascript') ) {
			$('#lievo-get-javascript').trigger('click');
		};
	});

	//paste shortcode
	$('#lievo-paste-shortcode').on('click', function () {
		var result_code;
		if ( $('#compact_shortcode_paste').is(':checked') ) {
			result_code = short_code_compact;
		} else {
			result_code = short_code_full;
		};
		window.send_to_editor( result_code );	
		$('#lievo-dialog-close-btn').trigger('click');
	});

	function clipboardAction() {
		if (clipboard) {
			clipboard.destroy();
		};
		clipboard = new Clipboard('#lievo-copy-code', {
			target: function() {
				return document.querySelector('#lievo-code');
			}
		});
		clipboard.on('success', function(e) {
			$('#lievo-copy-code').tooltip({content:'Copied!', trigger:'manual', placement:'left'});
			$('#lievo-copy-code').tooltip('show');
			$('#lievo-copy-code').on('mouseleave', function () {
				$('#lievo-copy-code').tooltip('destroy');
				$('#lievo-copy-code:focus').blur();
				e.clearSelection();
			});
		});
		clipboard.on('error', function(e) {
			var actionMsg = '';
			if(/iPhone|iPad/i.test(navigator.userAgent)) {
				actionMsg = 'No support :(';
			} else if (/Mac/i.test(navigator.userAgent)) {
				actionMsg = 'Press CMD+C to copy';
			} else {
				actionMsg = 'Press Ctrl+C to copy';
			};
			$('#lievo-copy-code').tooltip({content: actionMsg, trigger:'manual', placement:'left'});
			$('#lievo-copy-code').tooltip('show');
			$('#lievo-copy-code').on('mouseleave', function () {
				$('#lievo-copy-code').tooltip('destroy');
				$('#lievo-copy-code:focus').blur();
			});
		});
	};

	function update() {
		values.afterUpdate = function () {
			var temp = $('#lievo-curicon').liviconEvoOptions();

			if (values.addID !== '') {
				var add_id = ' id="' + values.addID + '"';
				var add_id_srt = ' add_id="' + values.addID + '"';
			} else {
				var add_id = '';
				var add_id_srt = '';
			};

			if (values.addClass !== '') {
				var add_class = ' ' + values.addClass;
				var add_class_srt = ' add_class="' + values.addClass +'"';
			} else {
				var add_class = '';
				var add_class_srt = '';
			};

			if (values.addCss !== '') {
				var add_css = ' style="' + values.addCss + '"';
				var add_css_srt = ' add_css="' + values.addCss + '"';
			} else {
				var add_css = '';
				var add_css_srt = '';
			};

			//HTML code
			html_code_compact = '<span' + add_id + ' class="livicon-evo' + add_class + '"' + add_css + ' data-options="\n' +
				'  name: '+ temp.name +'; \n' +
				(temp.style === d_o.style ? '' : '  style: '+ temp.style +'; \n') +
				(temp.size === d_o.size ? '' : '  size: '+ temp.size +'; \n') +
				(temp.strokeStyle === d_o.strokeStyle ? '' : '  strokeStyle: '+ temp.strokeStyle +'; \n') +
				(temp.strokeWidth === d_o.strokeWidth ? '' : '  strokeWidth: '+ temp.strokeWidth +'; \n') +
				(temp.tryToSharpen === d_o.tryToSharpen ? '' : '  tryToSharpen: '+ temp.tryToSharpen +'; \n') +
				(temp.rotate === d_o.rotate ? '' : '  rotate: '+ temp.rotate +'; \n') +
				(temp.flipHorizontal === d_o.flipHorizontal ? '' : '  flipHorizontal: '+ temp.flipHorizontal +'; \n') +
				(temp.flipVertical === d_o.flipVertical ? '' : '  flipVertical: '+ temp.flipVertical +'; \n') +
				(temp.strokeColor === d_o.strokeColor ? '' : '  strokeColor: '+ temp.strokeColor +'; \n') +
				(temp.strokeColorAction === d_o.strokeColorAction ? '' : '  strokeColorAction: '+ temp.strokeColorAction +'; \n') +
				(temp.strokeColorAlt === d_o.strokeColorAlt ? '' : '  strokeColorAlt: '+ temp.strokeColorAlt +'; \n') +
				(temp.strokeColorAltAction === d_o.strokeColorAltAction ? '' : '  strokeColorAltAction: '+ temp.strokeColorAltAction +'; \n') +
				(temp.fillColor === d_o.fillColor ? '' : '  fillColor: '+ temp.fillColor +'; \n') +
				(temp.fillColorAction === d_o.fillColorAction ? '' : '  fillColorAction: '+ temp.fillColorAction +'; \n') +
				(temp.solidColor === d_o.solidColor ? '' : '  solidColor: '+ temp.solidColor +'; \n') +
				(temp.solidColorAction === d_o.solidColorAction ? '' : '  solidColorAction: '+ temp.solidColorAction +'; \n') +
				(temp.solidColorBgAction === d_o.solidColorBgAction ? '' : '  solidColorBgAction: '+ temp.solidColorBgAction +'; \n') +
				(temp.solidColorBg === d_o.solidColorBg ? '' : '  solidColorBg: '+ temp.solidColorBg +'; \n') +
				(temp.colorsOnHover === d_o.colorsOnHover ? '' : '  colorsOnHover: '+ temp.colorsOnHover +'; \n') +
				(temp.colorsHoverTime === d_o.colorsHoverTime ? '' : '  colorsHoverTime: '+ temp.colorsHoverTime +'; \n') +
				(temp.colorsWhenMorph === d_o.colorsWhenMorph ? '' : '  colorsWhenMorph: '+ temp.colorsWhenMorph +'; \n') +
				(temp.brightness === d_o.brightness ? '' : '  brightness: '+ temp.brightness +'; \n') +
				(temp.saturation === d_o.saturation ? '' : '  saturation: '+ temp.saturation +'; \n') +
				(temp.morphState === d_o.morphState ? '' : '  morphState: '+ temp.morphState +'; \n') +
				(temp.morphImage === d_o.morphImage ? '' : '  morphImage: '+ temp.morphImage +'; \n') +
				(temp.allowMorphImageTransform === d_o.allowMorphImageTransform ? '' : '  allowMorphImageTransform: '+ temp.allowMorphImageTransform +'; \n') +
				(temp.strokeWidthFactorOnHover === d_o.strokeWidthFactorOnHover ? '' : '  strokeWidthFactorOnHover: '+ temp.strokeWidthFactorOnHover +'; \n') +
				(temp.strokeWidthOnHoverTime === d_o.strokeWidthOnHoverTime ? '' : '  strokeWidthOnHoverTime: '+ temp.strokeWidthOnHoverTime +'; \n') +
				(temp.keepStrokeWidthOnResize === d_o.keepStrokeWidthOnResize ? '' : '  keepStrokeWidthOnResize: '+ temp.keepStrokeWidthOnResize +'; \n') +
				(temp.animated === d_o.animated ? '' : '  animated: '+ temp.animated +'; \n') +
				(temp.eventType === d_o.eventType ? '' : '  eventType: '+ temp.eventType +'; \n') +
				(temp.eventOn === d_o.eventOn ? '' : '  eventOn: '+ temp.eventOn +'; \n') +
				(temp.autoPlay === d_o.autoPlay ? '' : '  autoPlay: '+ temp.autoPlay +'; \n') +
				(temp.delay === d_o.delay ? '' : '  delay: '+ temp.delay +'; \n') +
				(temp.duration === d_o.duration ? '' : '  duration: '+ temp.duration +'; \n') +
				(temp.repeat === d_o.repeat ? '' : '  repeat: '+ temp.repeat +'; \n') +
				(temp.repeatDelay === d_o.repeatDelay ? '' : '  repeatDelay: '+ temp.repeatDelay +'; \n') +
				(temp.drawOnViewport === d_o.drawOnViewport ? '' : '  drawOnViewport: '+ temp.drawOnViewport +'; \n') +
				(temp.viewportShift === d_o.viewportShift ? '' : '  viewportShift: '+ temp.viewportShift +'; \n') +
				(temp.drawDelay === d_o.drawDelay ? '' : '  drawDelay: '+ temp.drawDelay +'; \n') +
				(temp.drawTime === d_o.drawTime ? '' : '  drawTime: '+ temp.drawTime +'; \n') +
				(temp.drawStagger === d_o.drawStagger ? '' : '  drawStagger: '+ temp.drawStagger +'; \n') +
				(temp.drawStartPoint === d_o.drawStartPoint ? '' : '  drawStartPoint: '+ temp.drawStartPoint +'; \n') +
				(temp.drawColor === d_o.drawColor ? '' : '  drawColor: '+ temp.drawColor +'; \n') +
				(temp.drawColorTime === d_o.drawColorTime ? '' : '  drawColorTime: '+ temp.drawColorTime +'; \n') +
				(temp.drawReversed === d_o.drawReversed ? '' : '  drawReversed: '+ temp.drawReversed +'; \n') +
				(temp.drawEase === d_o.drawEase ? '' : '  drawEase: '+ temp.drawEase +'; \n') +
				(temp.eraseDelay === d_o.eraseDelay ? '' : '  eraseDelay: '+ temp.eraseDelay +'; \n') +
				(temp.eraseTime === d_o.eraseTime ? '' : '  eraseTime: '+ temp.eraseTime +'; \n') +
				(temp.eraseStagger === d_o.eraseStagger ? '' : '  eraseStagger: '+ temp.eraseStagger +'; \n') +
				(temp.eraseStartPoint === d_o.eraseStartPoint ? '' : '  eraseStartPoint: '+ temp.eraseStartPoint +'; \n') +
				(temp.eraseReversed === d_o.eraseReversed ? '' : '  eraseReversed: '+ temp.eraseReversed +'; \n') +
				(temp.eraseEase === d_o.eraseEase ? '' : '  eraseEase: '+ temp.eraseEase +'; \n') +
				(temp.touchEvents === d_o.touchEvents ? '' : '  touchEvents: '+ temp.touchEvents +'; \n') +
				'"></span>';
			//remove ending ";"
			html_code_compact = html_code_compact.replace(/;[\s]+"><\/span>/m, ' \n"></span>');
			html_code_compact_collapsed = html_code_compact.replace(/\r?\n|\r/g, '').replace(/\s\s+/g, ' ');

			html_code_full = '<span' + add_id + ' class="livicon-evo' + add_class + '"' + add_css + ' data-options="\n' +
				'  name: '+ temp.name +'; \n' +
				'  style: '+ temp.style +'; \n' +
				'  size: '+ temp.size +'; \n' +
				'  strokeStyle: '+ temp.strokeStyle +'; \n' +
				'  strokeWidth: '+ temp.strokeWidth +'; \n' +
				'  tryToSharpen: '+ temp.tryToSharpen +'; \n' +
				'  rotate: '+ temp.rotate +'; \n' +
				'  flipHorizontal: '+ temp.flipHorizontal +'; \n' +
				'  flipVertical: '+ temp.flipVertical +'; \n' +
				'  strokeColor: '+ temp.strokeColor +'; \n' +
				'  strokeColorAction: '+ temp.strokeColorAction +'; \n' +
				'  strokeColorAlt: '+ temp.strokeColorAlt +'; \n' +
				'  strokeColorAltAction: '+ temp.strokeColorAltAction +'; \n' +
				'  fillColor: '+ temp.fillColor +'; \n' +
				'  fillColorAction: '+ temp.fillColorAction +'; \n' +
				'  solidColor: '+ temp.solidColor +'; \n' +
				'  solidColorAction: '+ temp.solidColorAction +'; \n' +
				'  solidColorBgAction: '+ temp.solidColorBgAction +'; \n' +
				'  solidColorBg: '+ temp.solidColorBg +'; \n' +
				'  colorsOnHover: '+ temp.colorsOnHover +'; \n' +
				'  colorsHoverTime: '+ temp.colorsHoverTime +'; \n' +
				'  colorsWhenMorph: '+ temp.colorsWhenMorph +'; \n' +
				'  brightness: '+ temp.brightness +'; \n' +
				'  saturation: '+ temp.saturation +'; \n' +
				'  morphState: '+ temp.morphState +'; \n' +
				'  morphImage: '+ temp.morphImage +'; \n' +
				'  allowMorphImageTransform: '+ temp.allowMorphImageTransform +'; \n' +
				'  strokeWidthFactorOnHover: '+ temp.strokeWidthFactorOnHover +'; \n' +
				'  strokeWidthOnHoverTime: '+ temp.strokeWidthOnHoverTime +'; \n' +
				'  keepStrokeWidthOnResize: '+ temp.keepStrokeWidthOnResize +'; \n' +
				'  animated: '+ temp.animated +'; \n' +
				'  eventType: '+ temp.eventType +'; \n' +
				'  eventOn: '+ temp.eventOn +'; \n' +
				'  autoPlay: '+ temp.autoPlay +'; \n' +
				'  delay: '+ temp.delay +'; \n' +
				'  duration: '+ temp.duration +'; \n' +
				'  repeat: '+ temp.repeat +'; \n' +
				'  repeatDelay: '+ temp.repeatDelay +'; \n' +
				'  drawOnViewport: '+ temp.drawOnViewport +'; \n' +
				'  viewportShift: '+ temp.viewportShift +'; \n' +
				'  drawDelay: '+ temp.drawDelay +'; \n' +
				'  drawTime: '+ temp.drawTime +'; \n' +
				'  drawStagger: '+ temp.drawStagger +'; \n' +
				'  drawStartPoint: '+ temp.drawStartPoint +'; \n' +
				'  drawColor: '+ temp.drawColor +'; \n' +
				'  drawColorTime: '+ temp.drawColorTime +'; \n' +
				'  drawReversed: '+ temp.drawReversed +'; \n' +
				'  drawEase: '+ values.drawEase +'; \n' +
				'  eraseDelay: '+ temp.eraseDelay +'; \n' +
				'  eraseTime: '+ temp.eraseTime +'; \n' +
				'  eraseStagger: '+ temp.eraseStagger +'; \n' +
				'  eraseStartPoint: '+ temp.eraseStartPoint +'; \n' +
				'  eraseReversed: '+ temp.eraseReversed +'; \n' +
				'  eraseEase: '+ values.eraseEase +'; \n' +
				'  touchEvents: '+ temp.touchEvents +' \n' +
				'"></span>';
			html_code_full_collapsed = html_code_full.replace(/\r?\n|\r/g, '').replace(/\s\s+/g, ' ');
			
			if (values.addLink !== '') {
				if (values.linkTarget === 'blank') {
					var trg = ' target="_blank"';
				} else {
					var trg = '';
				};
				html_code_compact = '<a class="livicon-evo-link" href="' + values.addLink + '"' + trg + '>' + html_code_compact + '</a>';
				html_code_compact_collapsed = '<a class="livicon-evo-link" href="' + values.addLink + '"' + trg + '>' + html_code_compact_collapsed + '</a>';
				html_code_full = '<a class="livicon-evo-link" href="' + values.addLink + '"' + trg + '>' + html_code_full + '</a>';
				html_code_full_collapsed = '<a class="livicon-evo-link" href="' + values.addLink + '"' + trg + '>' + html_code_full_collapsed + '</a>';
			};

			//JavaScript code
			js_code_compact = 'jQuery("css_selector").addLiviconEvo({\n' +
				'  name: "'+ temp.name +'", \n' +
				(temp.style === d_o.style ? '' : '  style: "'+ temp.style +'", \n') +
				(temp.size === d_o.size ? '' : '  size: "'+ temp.size +'", \n') +
				(temp.strokeStyle === d_o.strokeStyle ? '' : '  strokeStyle: "'+ temp.strokeStyle +'", \n') +
				(temp.strokeWidth === d_o.strokeWidth ? '' : '  strokeWidth: "'+ temp.strokeWidth +'", \n') +
				(temp.tryToSharpen === d_o.tryToSharpen ? '' : '  tryToSharpen: '+ temp.tryToSharpen +', \n') +
				(temp.rotate === d_o.rotate ? '' : '  rotate: "'+ temp.rotate +'", \n') +
				(temp.flipHorizontal === d_o.flipHorizontal ? '' : '  flipHorizontal: '+ temp.flipHorizontal +', \n') +
				(temp.flipVertical === d_o.flipVertical ? '' : '  flipVertical: '+ temp.flipVertical +', \n') +
				(temp.strokeColor === d_o.strokeColor ? '' : '  strokeColor: "'+ temp.strokeColor +'", \n') +
				(temp.strokeColorAction === d_o.strokeColorAction ? '' : '  strokeColorAction: "'+ temp.strokeColorAction +'", \n') +
				(temp.strokeColorAlt === d_o.strokeColorAlt ? '' : '  strokeColorAlt: "'+ temp.strokeColorAlt +'", \n') +
				(temp.strokeColorAltAction === d_o.strokeColorAltAction ? '' : '  strokeColorAltAction: "'+ temp.strokeColorAltAction +'", \n') +
				(temp.fillColor === d_o.fillColor ? '' : '  fillColor: "'+ temp.fillColor +'", \n') +
				(temp.fillColorAction === d_o.fillColorAction ? '' : '  fillColorAction: "'+ temp.fillColorAction +'", \n') +
				(temp.solidColor === d_o.solidColor ? '' : '  solidColor: "'+ temp.solidColor +'", \n') +
				(temp.solidColorAction === d_o.solidColorAction ? '' : '  solidColorAction: "'+ temp.solidColorAction +'", \n') +
				(temp.solidColorBg === d_o.solidColorBg ? '' : '  solidColorBg: "'+ temp.solidColorBg +'", \n') +
				(temp.solidColorBgAction === d_o.solidColorBgAction ? '' : '  solidColorBgAction: "'+ temp.solidColorBgAction +'", \n') +
				(temp.colorsOnHover === d_o.colorsOnHover ? '' : '  colorsOnHover: "'+ temp.colorsOnHover +'", \n') +
				(temp.colorsHoverTime === d_o.colorsHoverTime ? '' : '  colorsHoverTime: '+ temp.colorsHoverTime +', \n') +
				(temp.colorsWhenMorph === d_o.colorsWhenMorph ? '' : '  colorsWhenMorph: "'+ temp.colorsWhenMorph +'", \n') +
				(temp.brightness === d_o.brightness ? '' : '  brightness: '+ temp.brightness +', \n') +
				(temp.saturation === d_o.saturation ? '' : '  saturation: '+ temp.saturation +', \n') +
				(temp.morphState === d_o.morphState ? '' : '  morphState: "'+ temp.morphState +'", \n') +
				(temp.morphImage === d_o.morphImage ? '' : '  morphImage: "'+ temp.morphImage +'", \n') +
				(temp.allowMorphImageTransform === d_o.allowMorphImageTransform ? '' : '  allowMorphImageTransform: '+ temp.allowMorphImageTransform +', \n') +
				(temp.strokeWidthFactorOnHover === d_o.strokeWidthFactorOnHover ? '' : '  strokeWidthFactorOnHover: "'+ temp.strokeWidthFactorOnHover +'", \n') +
				(temp.strokeWidthOnHoverTime === d_o.strokeWidthOnHoverTime ? '' : '  strokeWidthOnHoverTime: '+ temp.strokeWidthOnHoverTime +', \n') +
				(temp.keepStrokeWidthOnResize === d_o.keepStrokeWidthOnResize ? '' : '  keepStrokeWidthOnResize: '+ temp.keepStrokeWidthOnResize +', \n') +
				(temp.animated === d_o.animated ? '' : '  animated: '+ temp.animated +', \n') +
				(temp.eventType === d_o.eventType ? '' : '  eventType: "'+ temp.eventType +'", \n') +
				(temp.eventOn === d_o.eventOn ? '' : '  eventOn: "'+ temp.eventOn +'", \n') +
				(temp.autoPlay === d_o.autoPlay ? '' : '  autoPlay: '+ temp.autoPlay +', \n') +
				(temp.delay === d_o.delay ? '' : '  delay: '+ temp.delay +', \n') +
				(temp.duration === d_o.duration ? '' : '  duration: "'+ temp.duration +'", \n') +
				(temp.repeat === d_o.repeat ? '' : '  repeat: "'+ temp.repeat +'", \n') +
				(temp.repeatDelay === d_o.repeatDelay ? '' : '  repeatDelay: "'+ temp.repeatDelay +'", \n') +
				(temp.drawOnViewport === d_o.drawOnViewport ? '' : '  drawOnViewport: '+ temp.drawOnViewport +', \n') +
				(temp.viewportShift === d_o.viewportShift ? '' : '  viewportShift: "'+ temp.viewportShift +'", \n') +
				(temp.drawDelay === d_o.drawDelay ? '' : '  drawDelay: '+ temp.drawDelay +', \n') +
				(temp.drawTime === d_o.drawTime ? '' : '  drawTime: '+ temp.drawTime +', \n') +
				(temp.drawStagger === d_o.drawStagger ? '' : '  drawStagger: '+ temp.drawStagger +', \n') +
				(temp.drawStartPoint === d_o.drawStartPoint ? '' : '  drawStartPoint: "'+ temp.drawStartPoint +'", \n') +
				(temp.drawColor === d_o.drawColor ? '' : '  drawColor: "'+ temp.drawColor +'", \n') +
				(temp.drawColorTime === d_o.drawColorTime ? '' : '  drawColorTime: '+ temp.drawColorTime +', \n') +
				(temp.drawReversed === d_o.drawReversed ? '' : '  drawReversed: '+ temp.drawReversed +', \n') +
				(temp.drawEase === d_o.drawEase ? '' : '  drawEase: "'+ temp.drawEase +'", \n') +
				(temp.eraseDelay === d_o.eraseDelay ? '' : '  eraseDelay: '+ temp.eraseDelay +', \n') +
				(temp.eraseTime === d_o.eraseTime ? '' : '  eraseTime: '+ temp.eraseTime +', \n') +
				(temp.eraseStagger === d_o.eraseStagger ? '' : '  eraseStagger: '+ temp.eraseStagger +', \n') +
				(temp.eraseStartPoint === d_o.eraseStartPoint ? '' : '  eraseStartPoint: "'+ temp.eraseStartPoint +'", \n') +
				(temp.eraseReversed === d_o.eraseReversed ? '' : '  eraseReversed: '+ temp.eraseReversed +', \n') +
				(temp.eraseEase === d_o.eraseEase ? '' : '  eraseEase: "'+ temp.eraseEase +'", \n') +
				(temp.touchEvents === d_o.touchEvents ? '' : '  touchEvents: '+ temp.touchEvents +', \n') +
			'});';
			//remove ending ","
			js_code_compact = js_code_compact.replace(/,[\s]+}\);/m, ' \n});');
			js_code_compact_collapsed = js_code_compact.replace(/\r?\n|\r/g, '').replace(/\s\s+/g, ' ');

			js_code_full = 'jQuery("css_selector").addLiviconEvo({\n' +
				'  name: "'+ temp.name +'", \n' +
				'  style: "'+ temp.style +'", \n' +
				'  size: "'+ temp.size +'", \n' +
				'  strokeStyle: "'+ temp.strokeStyle +'", \n' +
				'  strokeWidth: "'+ temp.strokeWidth +'", \n' +
				'  tryToSharpen: '+ temp.tryToSharpen +', \n' +
				'  rotate: "'+ temp.rotate +'", \n' +
				'  flipHorizontal: '+ temp.flipHorizontal +', \n' +
				'  flipVertical: '+ temp.flipVertical +', \n' +
				'  strokeColor: "'+ temp.strokeColor +'", \n' +
				'  strokeColorAction: "'+ temp.strokeColorAction +'", \n' +
				'  strokeColorAlt: "'+ temp.strokeColorAlt +'", \n' +
				'  strokeColorAltAction: "'+ temp.strokeColorAltAction +'", \n' +
				'  fillColor: "'+ temp.fillColor +'", \n' +
				'  fillColorAction: "'+ temp.fillColorAction +'", \n' +
				'  solidColor: "'+ temp.solidColor +'", \n' +
				'  solidColorAction: "'+ temp.solidColorAction +'", \n' +
				'  solidColorBg: "'+ temp.solidColorBg +'", \n' +
				'  solidColorBgAction: "'+ temp.solidColorBgAction +'", \n' +
				'  colorsOnHover: "'+ temp.colorsOnHover +'", \n' +
				'  colorsHoverTime: '+ temp.colorsHoverTime +', \n' +
				'  colorsWhenMorph: "'+ temp.colorsWhenMorph +'", \n' +
				'  brightness: '+ temp.brightness +', \n' +
				'  saturation: '+ temp.saturation +', \n' +
				'  morphState: "'+ temp.morphState +'", \n' +
				'  morphImage: "'+ temp.morphImage +'", \n' +
				'  allowMorphImageTransform: '+ temp.allowMorphImageTransform +', \n' +
				'  strokeWidthFactorOnHover: "'+ temp.strokeWidthFactorOnHover +'", \n' +
				'  strokeWidthOnHoverTime: '+ temp.strokeWidthOnHoverTime +', \n' +
				'  keepStrokeWidthOnResize: '+ temp.keepStrokeWidthOnResize +', \n' +
				'  animated: '+ temp.animated +', \n' +
				'  eventType: "'+ temp.eventType +'", \n' +
				'  eventOn: "'+ temp.eventOn +'", \n' +
				'  autoPlay: '+ temp.autoPlay +', \n' +
				'  delay: '+ temp.delay +', \n' +
				'  duration: "'+ temp.duration +'", \n' +
				'  repeat: "'+ temp.repeat +'", \n' +
				'  repeatDelay: "'+ temp.repeatDelay +'", \n' +
				'  drawOnViewport: '+ temp.drawOnViewport +', \n' +
				'  viewportShift: "'+ temp.viewportShift +'", \n' +
				'  drawDelay: '+ temp.drawDelay +', \n' +
				'  drawTime: '+ temp.drawTime +', \n' +
				'  drawStagger: '+ temp.drawStagger +', \n' +
				'  drawStartPoint: "'+ temp.drawStartPoint +'", \n' +
				'  drawColor: "'+ temp.drawColor +'", \n' +
				'  drawColorTime: '+ temp.drawColorTime +', \n' +
				'  drawReversed: '+ temp.drawReversed +', \n' +
				'  drawEase: "'+ values.drawEase +'", \n' +
				'  eraseDelay: '+ temp.eraseDelay +', \n' +
				'  eraseTime: '+ temp.eraseTime +', \n' +
				'  eraseStagger: '+ temp.eraseStagger +', \n' +
				'  eraseStartPoint: "'+ temp.eraseStartPoint +'", \n' +
				'  eraseReversed: '+ temp.eraseReversed +', \n' +
				'  eraseEase: "'+ values.eraseEase +'", \n' +
				'  touchEvents: '+ temp.touchEvents +'\n' +
			'});';
			js_code_full_collapsed = js_code_full.replace(/\r?\n|\r/g, '').replace(/\s\s+/g, ' ');

			//Shortcode
			if (values.addLink !== '') {
				var link_srt = ' link="' + values.addLink + '"';
			} else {
				var link_srt = '';
			};
			if (values.linkTarget === 'blank') {
				var target_srt = ' target="blank"';
			} else {
				var target_srt = '';
			};

			short_code_compact = '[livicon_evo' +
				add_id_srt + 
				add_class_srt + 
				add_css_srt +
				link_srt +
				target_srt +
				' name="'+ temp.name +'"' +
				(temp.style === d_o.style ? '' : ' style="'+ temp.style +'"') +
				(temp.size === d_o.size ? '' : ' size="'+ temp.size +'"') +
				(temp.strokeStyle === d_o.strokeStyle ? '' : ' stroke_style="'+ temp.strokeStyle +'"') +
				(temp.strokeWidth === d_o.strokeWidth ? '' : ' stroke_width="'+ temp.strokeWidth +'"') +
				(temp.tryToSharpen === d_o.tryToSharpen ? '' : ' try_to_sharpen="'+ temp.tryToSharpen +'"') +
				(temp.rotate === d_o.rotate ? '' : ' rotate="'+ temp.rotate +'"') +
				(temp.flipHorizontal === d_o.flipHorizontal ? '' : ' flip_horizontal="'+ temp.flipHorizontal +'"') +
				(temp.flipVertical === d_o.flipVertical ? '' : ' flip_vertical="'+ temp.flipVertical +'"') +
				(temp.strokeColor === d_o.strokeColor ? '' : ' stroke_color="'+ temp.strokeColor +'"') +
				(temp.strokeColorAction === d_o.strokeColorAction ? '' : ' stroke_color_action="'+ temp.strokeColorAction +'"') +
				(temp.strokeColorAlt === d_o.strokeColorAlt ? '' : ' stroke_color_alt="'+ temp.strokeColorAlt +'"') +
				(temp.strokeColorAltAction === d_o.strokeColorAltAction ? '' : ' stroke_color_alt_action="'+ temp.strokeColorAltAction +'"') +
				(temp.fillColor === d_o.fillColor ? '' : ' fill_color="'+ temp.fillColor +'"') +
				(temp.fillColorAction === d_o.fillColorAction ? '' : ' fill_color_action="'+ temp.fillColorAction +'"') +
				(temp.solidColor === d_o.solidColor ? '' : ' solid_color="'+ temp.solidColor +'"') +
				(temp.solidColorBg === d_o.solidColorBg ? '' : ' solid_color_bg="'+ temp.solidColorBg +'"') +
				(temp.solidColorAction === d_o.solidColorAction ? '' : ' solid_color_action="'+ temp.solidColorAction +'"') +
				(temp.solidColorBgAction === d_o.solidColorBgAction ? '' : ' solid_color_bg_action="'+ temp.solidColorBgAction +'"') +
				(temp.colorsOnHover === d_o.colorsOnHover ? '' : ' colors_on_hover="'+ temp.colorsOnHover +'"') +
				(temp.colorsHoverTime === d_o.colorsHoverTime ? '' : ' colors_hover_time="'+ temp.colorsHoverTime +'"') +
				(temp.colorsWhenMorph === d_o.colorsWhenMorph ? '' : ' colors_when_morph="'+ temp.colorsWhenMorph +'"') +
				(temp.brightness === d_o.brightness ? '' : ' brightness="'+ temp.brightness +'"') +
				(temp.saturation === d_o.saturation ? '' : ' saturation="'+ temp.saturation +'"') +
				(temp.morphState === d_o.morphState ? '' : ' morph_state="'+ temp.morphState +'"') +
				(temp.morphImage === d_o.morphImage ? '' : ' morph_image="'+ temp.morphImage +'"') +
				(temp.allowMorphImageTransform === d_o.allowMorphImageTransform ? '' : ' allow_morph_image_transform="'+ temp.allowMorphImageTransform +'"') +
				(temp.strokeWidthFactorOnHover === d_o.strokeWidthFactorOnHover ? '' : ' stroke_width_factor_on_hover="'+ temp.strokeWidthFactorOnHover +'"') +
				(temp.strokeWidthOnHoverTime === d_o.strokeWidthOnHoverTime ? '' : ' stroke_width_on_hover_time="'+ temp.strokeWidthOnHoverTime +'"') +
				(temp.keepStrokeWidthOnResize === d_o.keepStrokeWidthOnResize ? '' : ' keep_stroke_width_on_resize="'+ temp.keepStrokeWidthOnResize +'"') +
				(temp.animated === d_o.animated ? '' : ' animated="'+ temp.animated +'"') +
				(temp.eventType === d_o.eventType ? '' : ' event_type="'+ temp.eventType +'"') +
				(temp.eventOn === d_o.eventOn ? '' : ' event_on="'+ temp.eventOn +'"') +
				(temp.autoPlay === d_o.autoPlay ? '' : ' auto_play="'+ temp.autoPlay +'"') +
				(temp.delay === d_o.delay ? '' : ' delay="'+ temp.delay +'"') +
				(temp.duration === d_o.duration ? '' : ' duration="'+ temp.duration +'"') +
				(temp.repeat === d_o.repeat ? '' : ' repeat="'+ temp.repeat +'"') +
				(temp.repeatDelay === d_o.repeatDelay ? '' : ' repeat_delay="'+ temp.repeatDelay +'"') +
				(temp.drawOnViewport === d_o.drawOnViewport ? '' : ' draw_on_viewport="'+ temp.drawOnViewport +'"') +
				(temp.viewportShift === d_o.viewportShift ? '' : ' viewport_shift="'+ temp.viewportShift +'"') +
				(temp.drawDelay === d_o.drawDelay ? '' : ' draw_delay="'+ temp.drawDelay +'"') +
				(temp.drawTime === d_o.drawTime ? '' : ' draw_time="'+ temp.drawTime +'"') +
				(temp.drawStagger === d_o.drawStagger ? '' : ' draw_stagger="'+ temp.drawStagger +'"') +
				(temp.drawStartPoint === d_o.drawStartPoint ? '' : ' draw_start_point="'+ temp.drawStartPoint +'"') +
				(temp.drawColor === d_o.drawColor ? '' : ' draw_color="'+ temp.drawColor +'"') +
				(temp.drawColorTime === d_o.drawColorTime ? '' : ' draw_color_time="'+ temp.drawColorTime +'"') +
				(temp.drawReversed === d_o.drawReversed ? '' : ' draw_reversed="'+ temp.drawReversed +'"') +
				(temp.drawEase === d_o.drawEase ? '' : ' draw_ease="'+ temp.drawEase +'"') +
				(temp.eraseDelay === d_o.eraseDelay ? '' : ' erase_delay="'+ temp.eraseDelay +'"') +
				(temp.eraseTime === d_o.eraseTime ? '' : ' erase_time="'+ temp.eraseTime +'"') +
				(temp.eraseStagger === d_o.eraseStagger ? '' : ' erase_stagger="'+ temp.eraseStagger +'"') +
				(temp.eraseStartPoint === d_o.eraseStartPoint ? '' : ' erase_start_point="'+ temp.eraseStartPoint +'"') +
				(temp.eraseReversed === d_o.eraseReversed ? '' : ' erase_reversed="'+ temp.eraseReversed +'"') +
				(temp.eraseEase === d_o.eraseEase ? '' : ' erase_ease="'+ temp.eraseEase +'"') +
				(temp.touchEvents === d_o.touchEvents ? '' : ' touch_events="'+ temp.touchEvents +'"') +
				'][/livicon_evo]';

			short_code_full = '[livicon_evo' +
				add_id_srt + 
				add_class_srt + 
				add_css_srt +
				link_srt +
				target_srt +
				' name="'+ temp.name +'"' +
				' style="'+ temp.style +'"' +
				' size="'+ temp.size +'"' +
				' stroke_style="'+ temp.strokeStyle +'"' +
				' stroke_width="'+ temp.strokeWidth +'"' +
				' try_to_sharpen="'+ temp.tryToSharpen +'"' +
				' rotate="'+ temp.rotate +'"' +
				' flip_horizontal="'+ temp.flipHorizontal +'"' +
				' flip_vertical="'+ temp.flipVertical +'"' +
				' stroke_color="'+ temp.strokeColor +'"' +
				' stroke_color_action="'+ temp.strokeColorAction +'"' +
				' stroke_color_alt="'+ temp.strokeColorAlt +'"' +
				' stroke_color_alt_action="'+ temp.strokeColorAltAction +'"' +
				' fill_color="'+ temp.fillColor +'"' +
				' fill_color_action="'+ temp.fillColorAction +'"' +
				' solid_color="'+ temp.solidColor +'"' +
				' solid_color_action="'+ temp.solidColorAction +'"' +
				' solid_color_bg="'+ temp.solidColorBg +'"' +
				' solid_color_bg_action="'+ temp.solidColorBgAction +'"' +
				' colors_on_hover="'+ temp.colorsOnHover +'"' +
				' colors_hover_time="'+ temp.colorsHoverTime +'"' +
				' colors_when_morph="'+ temp.colorsWhenMorph +'"' +
				' brightness="'+ temp.brightness +'"' +
				' saturation="'+ temp.saturation +'"' +
				' morph_state="'+ temp.morphState +'"' +
				' morph_image="'+ temp.morphImage +'"' +
				' allow_morph_image_transform="'+ temp.allowMorphImageTransform +'"' +
				' stroke_width_factor_on_hover="'+ temp.strokeWidthFactorOnHover +'"' +
				' stroke_width_on_hover_time="'+ temp.strokeWidthOnHoverTime +'"' +
				' keep_stroke_width_on_resize="'+ temp.keepStrokeWidthOnResize +'"' +
				' animated="'+ temp.animated +'"' +
				' event_type="'+ temp.eventType +'"' +
				' event_on="'+ temp.eventOn +'"' +
				' auto_play="'+ temp.autoPlay +'"' +
				' delay="'+ temp.delay +'"' +
				' duration="'+ temp.duration +'"' +
				' repeat="'+ temp.repeat +'"' +
				' repeat_delay="'+ temp.repeatDelay +'"' +
				' draw_on_viewport="'+ temp.drawOnViewport +'"' +
				' viewport_shift="'+ temp.viewportShift +'"' +
				' draw_delay="'+ temp.drawDelay +'"' +
				' draw_time="'+ temp.drawTime +'"' +
				' draw_stagger="'+ temp.drawStagger +'"' +
				' draw_start_point="'+ temp.drawStartPoint +'"' +
				' draw_color="'+ temp.drawColor +'"' +
				' draw_color_time="'+ temp.drawColorTime +'"' +
				' draw_reversed="'+ temp.drawReversed +'"' +
				' draw_ease="'+ values.drawEase +'"' +
				' erase_delay="'+ temp.eraseDelay +'"' +
				' erase_time="'+ temp.eraseTime +'"' +
				' erase_stagger="'+ temp.eraseStagger +'"' +
				' erase_start_point="'+ temp.eraseStartPoint +'"' +
				' erase_reversed="'+ temp.eraseReversed +'"' +
				' erase_ease="'+ values.eraseEase +'"' +
				' touch_events="'+ temp.touchEvents +'"' +
				'][/livicon_evo]';
			
			//set dynamic margin
			var h = $('#lievo-curicon').height();
			$('#lievo-curicon').css('margin-top', ((278-h)/2) +'px');
		};
		$('#lievo-curicon').updateLiviconEvo(values);
	};

	//equal height
	function equalHeight (el1, el2, delta) {
		$(el1).css( 'height', $(el2).innerHeight()-delta + 'px' );
	}
	$(window).on('resize', function () {
		equalHeight( '#lievo-icons', '#lievo-dialog-wrap', 115 );
		equalHeight( '#lievo-options', '#lievo-dialog-wrap', 150 );
	});

});//end doc ready


// Bootstrap popover
// License: MIT
!function(c){function r(){var b=document.createElement("bootstrap"),a={WebkitTransition:"webkitTransitionEnd",MozTransition:"transitionend",OTransition:"oTransitionEnd otransitionend",transition:"transitionend"},d;for(d in a)if(void 0!==b.style[d])return{end:a[d]};return!1}function n(b){return this.each(function(){var a=c(this),d=a.data("bs.tooltip"),g="object"==typeof b&&b;if(d||!/destroy|hide/.test(b))if(d||a.data("bs.tooltip",d=new e(this,g)),"string"==typeof b)d[b]()})}function n(b){return this.each(function(){var a=
c(this),d=a.data("bs.popover"),g="object"==typeof b&&b;if(d||!/destroy|hide/.test(b))if(d||a.data("bs.popover",d=new f(this,g)),"string"==typeof b)d[b]()})}c.fn.emulateTransitionEnd=function(b){var a=!1,d=this;c(this).one("bsTransitionEnd",function(){a=!0});setTimeout(function(){a||c(d).trigger(c.support.transition.end)},b);return this};c(function(){c.support.transition=r();c.support.transition&&(c.event.special.bsTransitionEnd={bindType:c.support.transition.end,delegateType:c.support.transition.end,
handle:function(b){if(c(b.target).is(this))return b.handleObj.handler.apply(this,arguments)}})});var e=function(b,a){this.inState=this.$element=this.hoverState=this.timeout=this.enabled=this.options=this.type=null;this.init("tooltip",b,a)};e.VERSION="3.3.6";e.TRANSITION_DURATION=150;e.DEFAULTS={animation:!0,placement:"top",selector:!1,template:'<div class="tooltip" role="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-inner"></div></div>',trigger:"hover focus",title:"",delay:0,html:!1,
container:!1,viewport:{selector:"body",padding:0}};e.prototype.init=function(b,a,d){this.enabled=!0;this.type=b;this.$element=c(a);this.options=this.getOptions(d);this.$viewport=this.options.viewport&&c(c.isFunction(this.options.viewport)?this.options.viewport.call(this,this.$element):this.options.viewport.selector||this.options.viewport);this.inState={click:!1,hover:!1,focus:!1};if(this.$element[0]instanceof document.constructor&&!this.options.selector)throw Error("`selector` option must be specified when initializing "+
this.type+" on the window.document object!");b=this.options.trigger.split(" ");for(a=b.length;a--;)if(d=b[a],"click"==d)this.$element.on("click."+this.type,this.options.selector,c.proxy(this.toggle,this));else if("manual"!=d){var g="hover"==d?"mouseleave":"focusout";this.$element.on(("hover"==d?"mouseenter":"focusin")+"."+this.type,this.options.selector,c.proxy(this.enter,this));this.$element.on(g+"."+this.type,this.options.selector,c.proxy(this.leave,this))}this.options.selector?this._options=c.extend({},
this.options,{trigger:"manual",selector:""}):this.fixTitle()};e.prototype.getDefaults=function(){return e.DEFAULTS};e.prototype.getOptions=function(b){b=c.extend({},this.getDefaults(),this.$element.data(),b);b.delay&&"number"==typeof b.delay&&(b.delay={show:b.delay,hide:b.delay});return b};e.prototype.getDelegateOptions=function(){var b={},a=this.getDefaults();this._options&&c.each(this._options,function(d,c){a[d]!=c&&(b[d]=c)});return b};e.prototype.enter=function(b){var a=b instanceof this.constructor?
b:c(b.currentTarget).data("bs."+this.type);a||(a=new this.constructor(b.currentTarget,this.getDelegateOptions()),c(b.currentTarget).data("bs."+this.type,a));b instanceof c.Event&&(a.inState["focusin"==b.type?"focus":"hover"]=!0);if(a.tip().hasClass("in")||"in"==a.hoverState)a.hoverState="in";else{clearTimeout(a.timeout);a.hoverState="in";if(!a.options.delay||!a.options.delay.show)return a.show();a.timeout=setTimeout(function(){"in"==a.hoverState&&a.show()},a.options.delay.show)}};e.prototype.isInStateTrue=
function(){for(var b in this.inState)if(this.inState[b])return!0;return!1};e.prototype.leave=function(b){var a=b instanceof this.constructor?b:c(b.currentTarget).data("bs."+this.type);a||(a=new this.constructor(b.currentTarget,this.getDelegateOptions()),c(b.currentTarget).data("bs."+this.type,a));b instanceof c.Event&&(a.inState["focusout"==b.type?"focus":"hover"]=!1);if(!a.isInStateTrue()){clearTimeout(a.timeout);a.hoverState="out";if(!a.options.delay||!a.options.delay.hide)return a.hide();a.timeout=
setTimeout(function(){"out"==a.hoverState&&a.hide()},a.options.delay.hide)}};e.prototype.show=function(){var b=c.Event("show.bs."+this.type);if(this.hasContent()&&this.enabled){this.$element.trigger(b);var a=c.contains(this.$element[0].ownerDocument.documentElement,this.$element[0]);if(!b.isDefaultPrevented()&&a){var d=this,b=this.tip(),a=this.getUID(this.type);this.setContent();b.attr("id",a);this.$element.attr("aria-describedby",a);this.options.animation&&b.addClass("fade");var a="function"==typeof this.options.placement?
this.options.placement.call(this,b[0],this.$element[0]):this.options.placement,g=/\s?auto?\s?/i,l=g.test(a);l&&(a=a.replace(g,"")||"top");b.detach().css({top:0,left:0,display:"block"}).addClass(a).data("bs."+this.type,this);this.options.container?b.appendTo(this.options.container):b.insertAfter(this.$element);this.$element.trigger("inserted.bs."+this.type);var g=this.getPosition(),p=b[0].offsetWidth,h=b[0].offsetHeight;if(l){var l=a,f=this.getPosition(this.$viewport),a="bottom"==a&&g.bottom+h>f.bottom?
"top":"top"==a&&g.top-h<f.top?"bottom":"right"==a&&g.right+p>f.width?"left":"left"==a&&g.left-p<f.left?"right":a;b.removeClass(l).addClass(a)}g=this.getCalculatedOffset(a,g,p,h);this.applyPlacement(g,a);a=function(){var a=d.hoverState;d.$element.trigger("shown.bs."+d.type);d.hoverState=null;"out"==a&&d.leave(d)};c.support.transition&&this.$tip.hasClass("fade")?b.one("bsTransitionEnd",a).emulateTransitionEnd(e.TRANSITION_DURATION):a()}}};e.prototype.applyPlacement=function(b,a){var d=this.tip(),g=
d[0].offsetWidth,e=d[0].offsetHeight,f=parseInt(d.css("margin-top"),10),h=parseInt(d.css("margin-left"),10);isNaN(f)&&(f=0);isNaN(h)&&(h=0);b.top+=f;b.left+=h;c.offset.setOffset(d[0],c.extend({using:function(a){d.css({top:Math.round(a.top),left:Math.round(a.left)})}},b),0);d.addClass("in");var h=d[0].offsetWidth,m=d[0].offsetHeight;"top"==a&&m!=e&&(b.top=b.top+e-m);var k=this.getViewportAdjustedDelta(a,b,h,m);k.left?b.left+=k.left:b.top+=k.top;g=(f=/top|bottom/.test(a))?2*k.left-g+h:2*k.top-e+m;e=
f?"offsetWidth":"offsetHeight";d.offset(b);this.replaceArrow(g,d[0][e],f)};e.prototype.replaceArrow=function(b,a,d){this.arrow().css(d?"left":"top",50*(1-b/a)+"%").css(d?"top":"left","")};e.prototype.setContent=function(){var b=this.tip(),a=this.getTitle();b.find(".tooltip-inner")[this.options.html?"html":"text"](a);b.removeClass("fade in top bottom left right")};e.prototype.hide=function(b){function a(){"in"!=d.hoverState&&g.detach();d.$element.removeAttr("aria-describedby").trigger("hidden.bs."+
d.type);b&&b()}var d=this,g=c(this.$tip),f=c.Event("hide.bs."+this.type);this.$element.trigger(f);if(!f.isDefaultPrevented())return g.removeClass("in"),c.support.transition&&g.hasClass("fade")?g.one("bsTransitionEnd",a).emulateTransitionEnd(e.TRANSITION_DURATION):a(),this.hoverState=null,this};e.prototype.fixTitle=function(){var b=this.$element;(b.attr("title")||"string"!=typeof b.attr("data-original-title"))&&b.attr("data-original-title",b.attr("title")||"").attr("title","")};e.prototype.hasContent=
function(){return this.getTitle()};e.prototype.getPosition=function(b){b=b||this.$element;var a=b[0],d="BODY"==a.tagName,a=a.getBoundingClientRect();null==a.width&&(a=c.extend({},a,{width:a.right-a.left,height:a.bottom-a.top}));var e=d?{top:0,left:0}:b.offset();b={scroll:d?document.documentElement.scrollTop||document.body.scrollTop:b.scrollTop()};d=d?{width:c(window).width(),height:c(window).height()}:null;return c.extend({},a,b,d,e)};e.prototype.getCalculatedOffset=function(b,a,d,c){return"bottom"==
b?{top:a.top+a.height,left:a.left+a.width/2-d/2}:"top"==b?{top:a.top-c,left:a.left+a.width/2-d/2}:"left"==b?{top:a.top+a.height/2-c/2,left:a.left-d}:{top:a.top+a.height/2-c/2,left:a.left+a.width}};e.prototype.getViewportAdjustedDelta=function(b,a,d,c){var e={top:0,left:0};if(!this.$viewport)return e;var f=this.options.viewport&&this.options.viewport.padding||0,h=this.getPosition(this.$viewport);/right|left/.test(b)?(d=a.top-f-h.scroll,a=a.top+f-h.scroll+c,d<h.top?e.top=h.top-d:a>h.top+h.height&&(e.top=
h.top+h.height-a)):(c=a.left-f,a=a.left+f+d,c<h.left?e.left=h.left-c:a>h.right&&(e.left=h.left+h.width-a));return e};e.prototype.getTitle=function(){var b=this.$element,a=this.options;return b.attr("data-original-title")||("function"==typeof a.title?a.title.call(b[0]):a.title)};e.prototype.getUID=function(b){do b+=~~(1E6*Math.random());while(document.getElementById(b));return b};e.prototype.tip=function(){if(!this.$tip&&(this.$tip=c(this.options.template),1!=this.$tip.length))throw Error(this.type+
" `template` option must consist of exactly 1 top-level element!");return this.$tip};e.prototype.arrow=function(){return this.$arrow=this.$arrow||this.tip().find(".tooltip-arrow")};e.prototype.enable=function(){this.enabled=!0};e.prototype.disable=function(){this.enabled=!1};e.prototype.toggleEnabled=function(){this.enabled=!this.enabled};e.prototype.toggle=function(b){var a=this;b&&(a=c(b.currentTarget).data("bs."+this.type),a||(a=new this.constructor(b.currentTarget,this.getDelegateOptions()),c(b.currentTarget).data("bs."+
this.type,a)));b?(a.inState.click=!a.inState.click,a.isInStateTrue()?a.enter(a):a.leave(a)):a.tip().hasClass("in")?a.leave(a):a.enter(a)};e.prototype.destroy=function(){var b=this;clearTimeout(this.timeout);this.hide(function(){b.$element.off("."+b.type).removeData("bs."+b.type);b.$tip&&b.$tip.detach();b.$tip=null;b.$arrow=null;b.$viewport=null})};var q=c.fn.tooltip;c.fn.tooltip=n;c.fn.tooltip.Constructor=e;c.fn.tooltip.noConflict=function(){c.fn.tooltip=q;return this};var f=function(b,a){this.init("popover",
b,a)};if(!c.fn.tooltip)throw Error("Popover requires tooltip.js");f.VERSION="3.3.6";f.DEFAULTS=c.extend({},c.fn.tooltip.Constructor.DEFAULTS,{placement:"right",trigger:"click",content:"",template:'<div class="lievo-popover" role="tooltip"><h3 class="popover-title"></h3><div class="lievo-popover-content"></div></div>'});f.prototype=c.extend({},c.fn.tooltip.Constructor.prototype);f.prototype.constructor=f;f.prototype.getDefaults=function(){return f.DEFAULTS};f.prototype.setContent=function(){var b=
this.tip(),a=this.getTitle(),c=this.getContent();b.find(".popover-title")[this.options.html?"html":"text"](a);b.find(".lievo-popover-content").children().detach().end()[this.options.html?"string"==typeof c?"html":"append":"text"](c);b.removeClass("fade top bottom left right in");b.find(".popover-title").html()||b.find(".popover-title").hide()};f.prototype.hasContent=function(){return this.getTitle()||this.getContent()};f.prototype.getContent=function(){var b=this.$element,a=this.options;return b.attr("data-content")||
("function"==typeof a.content?a.content.call(b[0]):a.content)};f.prototype.arrow=function(){return this.$arrow=this.$arrow||this.tip().find(".arrow")};q=c.fn.popover;c.fn.popover=n;c.fn.popover.Constructor=f;c.fn.popover.noConflict=function(){c.fn.popover=q;return this}}(jQuery);


// Spectrum Colorpicker v1.8.0
// https://github.com/bgrins/spectrum
// Author: Brian Grinstead
// License: MIT
(function(factory){if(typeof define==="function"&&define.amd)define(["jquery"],factory);else if(typeof exports=="object"&&typeof module=="object")module.exports=factory(require("jquery"));else factory(jQuery)})(function($,undefined){var defaultOpts={beforeShow:noop,move:noop,change:noop,show:noop,hide:noop,color:false,flat:false,showInput:false,allowEmpty:false,showButtons:true,clickoutFiresChange:true,showInitial:false,showPalette:false,showPaletteOnly:false,hideAfterPaletteSelect:false,togglePaletteOnly:false,
showSelectionPalette:true,localStorageKey:false,appendTo:"body",maxSelectionSize:7,cancelText:"cancel",chooseText:"choose",togglePaletteMoreText:"more",togglePaletteLessText:"less",clearText:"Clear Color Selection",noColorSelectedText:"No Color Selected",preferredFormat:false,className:"",containerClassName:"",replacerClassName:"",showAlpha:false,theme:"sp-light",palette:[["#ffffff","#000000","#ff0000","#ff8000","#ffff00","#008000","#0000ff","#4b0082","#9400d3"]],selectionPalette:[],disabled:false,
offset:null},spectrums=[],IE=!!/msie/i.exec(window.navigator.userAgent),rgbaSupport=function(){function contains(str,substr){return!!~(""+str).indexOf(substr)}var elem=document.createElement("div");var style=elem.style;style.cssText="background-color:rgba(0,0,0,.5)";return contains(style.backgroundColor,"rgba")||contains(style.backgroundColor,"hsla")}(),replaceInput=["<div class='sp-replacer'>","<div class='sp-preview'><div class='sp-preview-inner'></div></div>","<div class='sp-dd'>&#9660;</div>",
"</div>"].join(""),markup=function(){var gradientFix="";if(IE)for(var i=1;i<=6;i++)gradientFix+="<div class='sp-"+i+"'></div>";return["<div class='sp-container sp-hidden'>","<div class='sp-palette-container'>","<div class='sp-palette sp-thumb sp-cf'></div>","<div class='sp-palette-button-container sp-cf'>","<button type='button' class='sp-palette-toggle'></button>","</div>","</div>","<div class='sp-picker-container'>","<div class='sp-top sp-cf'>","<div class='sp-fill'></div>","<div class='sp-top-inner'>",
"<div class='sp-color'>","<div class='sp-sat'>","<div class='sp-val'>","<div class='sp-dragger'></div>","</div>","</div>","</div>","<div class='sp-clear sp-clear-display'>","</div>","<div class='sp-hue'>","<div class='sp-slider'></div>",gradientFix,"</div>","</div>","<div class='sp-alpha'><div class='sp-alpha-inner'><div class='sp-alpha-handle'></div></div></div>","</div>","<div class='sp-initial sp-thumb sp-cf'></div>","<div class='sp-input-container sp-cf'>","<input class='sp-input' type='text' spellcheck='false'  />","</div>",
"<div class='sp-button-container sp-cf'>","<a class='sp-cancel' href='#'></a>","<button type='button' class='sp-choose button'></button>","</div>","</div>","</div>"].join("")}();function paletteTemplate(p,color,className,opts){var html=[];for(var i=0;i<p.length;i++){var current=p[i];if(current){var tiny=tinycolor(current);var c=tiny.toHsl().l<.5?"sp-thumb-el sp-thumb-dark":"sp-thumb-el sp-thumb-light";c+=tinycolor.equals(color,current)?" sp-thumb-active":"";var formattedString=tiny.toString(opts.preferredFormat||
"rgb");var swatchStyle=rgbaSupport?"background-color:"+tiny.toRgbString():"filter:"+tiny.toFilter();html.push('<span title="'+formattedString+'" data-color="'+tiny.toRgbString()+'" class="'+c+'"><span class="sp-thumb-inner" style="'+swatchStyle+';" /></span>')}else{var cls="sp-clear-display";html.push($("<div />").append($('<span data-color="" style="background-color:transparent;" class="'+cls+'"></span>').attr("title",opts.noColorSelectedText)).html())}}return"<div class='sp-cf "+className+"'>"+
html.join("")+"</div>"}function hideAll(){for(var i=0;i<spectrums.length;i++)if(spectrums[i])spectrums[i].hide()}function instanceOptions(o,callbackContext){var opts=$.extend({},defaultOpts,o);opts.callbacks={"move":bind(opts.move,callbackContext),"change":bind(opts.change,callbackContext),"show":bind(opts.show,callbackContext),"hide":bind(opts.hide,callbackContext),"beforeShow":bind(opts.beforeShow,callbackContext)};return opts}function spectrum(element,o){var opts=instanceOptions(o,element),flat=
opts.flat,showSelectionPalette=opts.showSelectionPalette,localStorageKey=opts.localStorageKey,theme=opts.theme,callbacks=opts.callbacks,resize=throttle(reflow,10),visible=false,isDragging=false,dragWidth=0,dragHeight=0,dragHelperHeight=0,slideHeight=0,slideWidth=0,alphaWidth=0,alphaSlideHelperWidth=0,slideHelperHeight=0,currentHue=0,currentSaturation=0,currentValue=0,currentAlpha=1,palette=[],paletteArray=[],paletteLookup={},selectionPalette=opts.selectionPalette.slice(0),maxSelectionSize=opts.maxSelectionSize,
draggingClass="sp-dragging",shiftMovementDirection=null;var doc=element.ownerDocument,body=doc.body,boundElement=$(element),disabled=false,container=$(markup,doc).addClass(theme),pickerContainer=container.find(".sp-picker-container"),dragger=container.find(".sp-color"),dragHelper=container.find(".sp-dragger"),slider=container.find(".sp-hue"),slideHelper=container.find(".sp-slider"),alphaSliderInner=container.find(".sp-alpha-inner"),alphaSlider=container.find(".sp-alpha"),alphaSlideHelper=container.find(".sp-alpha-handle"),
textInput=container.find(".sp-input"),paletteContainer=container.find(".sp-palette"),initialColorContainer=container.find(".sp-initial"),cancelButton=container.find(".sp-cancel"),clearButton=container.find(".sp-clear"),chooseButton=container.find(".sp-choose"),toggleButton=container.find(".sp-palette-toggle"),isInput=boundElement.is("input"),isInputTypeColor=isInput&&boundElement.attr("type")==="color"&&inputTypeColorSupport(),shouldReplace=isInput&&!flat,replacer=shouldReplace?$(replaceInput).addClass(theme).addClass(opts.className).addClass(opts.replacerClassName):
$([]),offsetElement=shouldReplace?replacer:boundElement,previewElement=replacer.find(".sp-preview-inner"),initialColor=opts.color||isInput&&boundElement.val(),colorOnShow=false,currentPreferredFormat=opts.preferredFormat,clickoutFiresChange=!opts.showButtons||opts.clickoutFiresChange,isEmpty=!initialColor,allowEmpty=opts.allowEmpty&&!isInputTypeColor;function applyOptions(){if(opts.showPaletteOnly)opts.showPalette=true;toggleButton.text(opts.showPaletteOnly?opts.togglePaletteMoreText:opts.togglePaletteLessText);
if(opts.palette){palette=opts.palette.slice(0);paletteArray=$.isArray(palette[0])?palette:[palette];paletteLookup={};for(var i=0;i<paletteArray.length;i++)for(var j=0;j<paletteArray[i].length;j++){var rgb=tinycolor(paletteArray[i][j]).toRgbString();paletteLookup[rgb]=true}}container.toggleClass("sp-flat",flat);container.toggleClass("sp-input-disabled",!opts.showInput);container.toggleClass("sp-alpha-enabled",opts.showAlpha);container.toggleClass("sp-clear-enabled",allowEmpty);container.toggleClass("sp-buttons-disabled",
!opts.showButtons);container.toggleClass("sp-palette-buttons-disabled",!opts.togglePaletteOnly);container.toggleClass("sp-palette-disabled",!opts.showPalette);container.toggleClass("sp-palette-only",opts.showPaletteOnly);container.toggleClass("sp-initial-disabled",!opts.showInitial);container.addClass(opts.className).addClass(opts.containerClassName);reflow()}function initialize(){if(IE)container.find("*:not(input)").attr("unselectable","on");applyOptions();if(shouldReplace)boundElement.after(replacer).hide();
if(!allowEmpty)clearButton.hide();if(flat)boundElement.after(container).hide();else{var appendTo=opts.appendTo==="parent"?boundElement.parent():$(opts.appendTo);if(appendTo.length!==1)appendTo=$("body");appendTo.append(container)}updateSelectionPaletteFromStorage();offsetElement.bind("click.spectrum touchstart.spectrum",function(e){if(!disabled)toggle();e.stopPropagation();if(!$(e.target).is("input"))e.preventDefault()});if(boundElement.is(":disabled")||opts.disabled===true)disable();container.click(stopPropagation);
textInput.change(setFromTextInput);textInput.bind("paste",function(){setTimeout(setFromTextInput,1)});textInput.keydown(function(e){if(e.keyCode==13)setFromTextInput()});cancelButton.text(opts.cancelText);cancelButton.bind("click.spectrum",function(e){e.stopPropagation();e.preventDefault();revert();hide()});clearButton.attr("title",opts.clearText);clearButton.bind("click.spectrum",function(e){e.stopPropagation();e.preventDefault();isEmpty=true;move();if(flat)updateOriginalInput(true)});chooseButton.text(opts.chooseText);
chooseButton.bind("click.spectrum",function(e){e.stopPropagation();e.preventDefault();if(IE&&textInput.is(":focus"))textInput.trigger("change");if(isValid()){updateOriginalInput(true);hide()}});toggleButton.text(opts.showPaletteOnly?opts.togglePaletteMoreText:opts.togglePaletteLessText);toggleButton.bind("click.spectrum",function(e){e.stopPropagation();e.preventDefault();opts.showPaletteOnly=!opts.showPaletteOnly;if(!opts.showPaletteOnly&&!flat)container.css("left","-="+(pickerContainer.outerWidth(true)+
5));applyOptions()});draggable(alphaSlider,function(dragX,dragY,e){currentAlpha=dragX/alphaWidth;isEmpty=false;if(e.shiftKey)currentAlpha=Math.round(currentAlpha*10)/10;move()},dragStart,dragStop);draggable(slider,function(dragX,dragY){currentHue=parseFloat(dragY/slideHeight);isEmpty=false;if(!opts.showAlpha)currentAlpha=1;move()},dragStart,dragStop);draggable(dragger,function(dragX,dragY,e){if(!e.shiftKey)shiftMovementDirection=null;else if(!shiftMovementDirection){var oldDragX=currentSaturation*
dragWidth;var oldDragY=dragHeight-currentValue*dragHeight;var furtherFromX=Math.abs(dragX-oldDragX)>Math.abs(dragY-oldDragY);shiftMovementDirection=furtherFromX?"x":"y"}var setSaturation=!shiftMovementDirection||shiftMovementDirection==="x";var setValue=!shiftMovementDirection||shiftMovementDirection==="y";if(setSaturation)currentSaturation=parseFloat(dragX/dragWidth);if(setValue)currentValue=parseFloat((dragHeight-dragY)/dragHeight);isEmpty=false;if(!opts.showAlpha)currentAlpha=1;move()},dragStart,
dragStop);if(!!initialColor){set(initialColor);updateUI();currentPreferredFormat=opts.preferredFormat||tinycolor(initialColor).format;addColorToSelectionPalette(initialColor)}else updateUI();if(flat)show();function paletteElementClick(e){if(e.data&&e.data.ignore){set($(e.target).closest(".sp-thumb-el").data("color"));move()}else{set($(e.target).closest(".sp-thumb-el").data("color"));move();updateOriginalInput(true);if(opts.hideAfterPaletteSelect)hide()}return false}var paletteEvent=IE?"mousedown.spectrum":
"click.spectrum touchstart.spectrum";paletteContainer.delegate(".sp-thumb-el",paletteEvent,paletteElementClick);initialColorContainer.delegate(".sp-thumb-el:nth-child(1)",paletteEvent,{ignore:true},paletteElementClick)}function updateSelectionPaletteFromStorage(){if(localStorageKey&&window.localStorage){try{var oldPalette=window.localStorage[localStorageKey].split(",#");if(oldPalette.length>1){delete window.localStorage[localStorageKey];$.each(oldPalette,function(i,c){addColorToSelectionPalette(c)})}}catch(e){}try{selectionPalette=
window.localStorage[localStorageKey].split(";")}catch(e$0){}}}function addColorToSelectionPalette(color){if(showSelectionPalette){var rgb=tinycolor(color).toRgbString();if(!paletteLookup[rgb]&&$.inArray(rgb,selectionPalette)===-1){selectionPalette.push(rgb);while(selectionPalette.length>maxSelectionSize)selectionPalette.shift()}if(localStorageKey&&window.localStorage)try{window.localStorage[localStorageKey]=selectionPalette.join(";")}catch(e){}}}function getUniqueSelectionPalette(){var unique=[];
if(opts.showPalette)for(var i=0;i<selectionPalette.length;i++){var rgb=tinycolor(selectionPalette[i]).toRgbString();if(!paletteLookup[rgb])unique.push(selectionPalette[i])}return unique.reverse().slice(0,opts.maxSelectionSize)}function drawPalette(){var currentColor=get();var html=$.map(paletteArray,function(palette,i){return paletteTemplate(palette,currentColor,"sp-palette-row sp-palette-row-"+i,opts)});updateSelectionPaletteFromStorage();if(selectionPalette)html.push(paletteTemplate(getUniqueSelectionPalette(),
currentColor,"sp-palette-row sp-palette-row-selection",opts));paletteContainer.html(html.join(""))}function drawInitial(){if(opts.showInitial){var initial=colorOnShow;var current=get();initialColorContainer.html(paletteTemplate([initial,current],current,"sp-palette-row-initial",opts))}}function dragStart(){if(dragHeight<=0||dragWidth<=0||slideHeight<=0)reflow();isDragging=true;container.addClass(draggingClass);shiftMovementDirection=null;boundElement.trigger("dragstart.spectrum",[get()])}function dragStop(){isDragging=
false;container.removeClass(draggingClass);boundElement.trigger("dragstop.spectrum",[get()])}function setFromTextInput(){var value=textInput.val();if((value===null||value==="")&&allowEmpty){set(null);updateOriginalInput(true)}else{var tiny=tinycolor(value);if(tiny.isValid()){set(tiny);updateOriginalInput(true)}else textInput.addClass("sp-validation-error")}}function toggle(){if(visible)hide();else show()}function show(){var event=$.Event("beforeShow.spectrum");if(visible){reflow();return}boundElement.trigger(event,
[get()]);if(callbacks.beforeShow(get())===false||event.isDefaultPrevented())return;hideAll();visible=true;$(doc).bind("keydown.spectrum",onkeydown);$(doc).bind("click.spectrum",clickout);$(window).bind("resize.spectrum",resize);replacer.addClass("sp-active");container.removeClass("sp-hidden");reflow();updateUI();colorOnShow=get();drawInitial();callbacks.show(colorOnShow);boundElement.trigger("show.spectrum",[colorOnShow])}function onkeydown(e){if(e.keyCode===27)hide()}function clickout(e){if(e.button==
2)return;if(isDragging)return;if(clickoutFiresChange)updateOriginalInput(true);else revert();hide()}function hide(){if(!visible||flat)return;visible=false;$(doc).unbind("keydown.spectrum",onkeydown);$(doc).unbind("click.spectrum",clickout);$(window).unbind("resize.spectrum",resize);replacer.removeClass("sp-active");container.addClass("sp-hidden");callbacks.hide(get());boundElement.trigger("hide.spectrum",[get()])}function revert(){set(colorOnShow,true)}function set(color,ignoreFormatChange){if(tinycolor.equals(color,
get())){updateUI();return}var newColor,newHsv;if(!color&&allowEmpty)isEmpty=true;else{isEmpty=false;newColor=tinycolor(color);newHsv=newColor.toHsv();currentHue=newHsv.h%360/360;currentSaturation=newHsv.s;currentValue=newHsv.v;currentAlpha=newHsv.a}updateUI();if(newColor&&newColor.isValid()&&!ignoreFormatChange)currentPreferredFormat=opts.preferredFormat||newColor.getFormat()}function get(opts){opts=opts||{};if(allowEmpty&&isEmpty)return null;return tinycolor.fromRatio({h:currentHue,s:currentSaturation,
v:currentValue,a:Math.round(currentAlpha*100)/100},{format:opts.format||currentPreferredFormat})}function isValid(){return!textInput.hasClass("sp-validation-error")}function move(){updateUI();callbacks.move(get());boundElement.trigger("move.spectrum",[get()])}function updateUI(){textInput.removeClass("sp-validation-error");updateHelperLocations();var flatColor=tinycolor.fromRatio({h:currentHue,s:1,v:1});dragger.css("background-color",flatColor.toHexString());var format=currentPreferredFormat;if(currentAlpha<
1&&!(currentAlpha===0&&format==="name"))if(format==="hex"||format==="hex3"||format==="hex6"||format==="name")format="rgb";var realColor=get({format:format}),displayColor="";previewElement.removeClass("sp-clear-display");previewElement.css("background-color","transparent");if(!realColor&&allowEmpty)previewElement.addClass("sp-clear-display");else{var realHex=realColor.toHexString(),realRgb=realColor.toRgbString();if(rgbaSupport||realColor.alpha===1)previewElement.css("background-color",realRgb);else{previewElement.css("background-color",
"transparent");previewElement.css("filter",realColor.toFilter())}if(opts.showAlpha){var rgb=realColor.toRgb();rgb.a=0;var realAlpha=tinycolor(rgb).toRgbString();var gradient="linear-gradient(left, "+realAlpha+", "+realHex+")";if(IE)alphaSliderInner.css("filter",tinycolor(realAlpha).toFilter({gradientType:1},realHex));else{alphaSliderInner.css("background","-webkit-"+gradient);alphaSliderInner.css("background","-moz-"+gradient);alphaSliderInner.css("background","-ms-"+gradient);alphaSliderInner.css("background",
"linear-gradient(to right, "+realAlpha+", "+realHex+")")}}displayColor=realColor.toString(format)}if(opts.showInput)textInput.val(displayColor);if(opts.showPalette)drawPalette();drawInitial()}function updateHelperLocations(){var s=currentSaturation;var v=currentValue;if(allowEmpty&&isEmpty){alphaSlideHelper.hide();slideHelper.hide();dragHelper.hide()}else{alphaSlideHelper.show();slideHelper.show();dragHelper.show();var dragX=s*dragWidth;var dragY=dragHeight-v*dragHeight;dragX=Math.max(-dragHelperHeight,
Math.min(dragWidth-dragHelperHeight,dragX-dragHelperHeight));dragY=Math.max(-dragHelperHeight,Math.min(dragHeight-dragHelperHeight,dragY-dragHelperHeight));dragHelper.css({"top":dragY+"px","left":dragX+"px"});var alphaX=currentAlpha*alphaWidth;alphaSlideHelper.css({"left":alphaX-alphaSlideHelperWidth/2+"px"});var slideY=currentHue*slideHeight;slideHelper.css({"top":slideY-slideHelperHeight+"px"})}}function updateOriginalInput(fireCallback){var color=get(),displayColor="",hasChanged=!tinycolor.equals(color,
colorOnShow);if(color){displayColor=color.toString(currentPreferredFormat);addColorToSelectionPalette(color)}if(isInput)boundElement.val(displayColor);if(fireCallback&&hasChanged){callbacks.change(color);boundElement.trigger("change",[color])}}function reflow(){if(!visible)return;dragWidth=dragger.width();dragHeight=dragger.height();dragHelperHeight=dragHelper.height();slideWidth=slider.width();slideHeight=slider.height();slideHelperHeight=slideHelper.height();alphaWidth=alphaSlider.width();alphaSlideHelperWidth=
alphaSlideHelper.width();if(!flat){container.css("position","absolute");if(opts.offset)container.offset(opts.offset);else container.offset(getOffset(container,offsetElement))}updateHelperLocations();if(opts.showPalette)drawPalette();boundElement.trigger("reflow.spectrum")}function destroy(){boundElement.show();offsetElement.unbind("click.spectrum touchstart.spectrum");container.remove();replacer.remove();spectrums[spect.id]=null}function option(optionName,optionValue){if(optionName===undefined)return $.extend({},
opts);if(optionValue===undefined)return opts[optionName];opts[optionName]=optionValue;if(optionName==="preferredFormat")currentPreferredFormat=opts.preferredFormat;applyOptions()}function enable(){disabled=false;boundElement.attr("disabled",false);offsetElement.removeClass("sp-disabled")}function disable(){hide();disabled=true;boundElement.attr("disabled",true);offsetElement.addClass("sp-disabled")}function setOffset(coord){opts.offset=coord;reflow()}initialize();var spect={show:show,hide:hide,toggle:toggle,
reflow:reflow,option:option,enable:enable,disable:disable,offset:setOffset,set:function(c){set(c);updateOriginalInput()},get:get,destroy:destroy,container:container};spect.id=spectrums.push(spect)-1;return spect}function getOffset(picker,input){var extraY=0;var dpWidth=picker.outerWidth();var dpHeight=picker.outerHeight();var inputHeight=input.outerHeight();var doc=picker[0].ownerDocument;var docElem=doc.documentElement;var viewWidth=docElem.clientWidth+$(doc).scrollLeft();var viewHeight=docElem.clientHeight+
$(doc).scrollTop();var offset=input.offset();offset.top+=inputHeight;offset.left-=Math.min(offset.left,offset.left+dpWidth>viewWidth&&viewWidth>dpWidth?Math.abs(offset.left+dpWidth-viewWidth):0);offset.top-=Math.min(offset.top,offset.top+dpHeight>viewHeight&&viewHeight>dpHeight?Math.abs(dpHeight+inputHeight-extraY):extraY);return offset}function noop(){}function stopPropagation(e){e.stopPropagation()}function bind(func,obj){var slice=Array.prototype.slice;var args=slice.call(arguments,2);return function(){return func.apply(obj,
args.concat(slice.call(arguments)))}}function draggable(element,onmove,onstart,onstop){onmove=onmove||function(){};onstart=onstart||function(){};onstop=onstop||function(){};var doc=document;var dragging=false;var offset={};var maxHeight=0;var maxWidth=0;var hasTouch="ontouchstart"in window;var duringDragEvents={};duringDragEvents["selectstart"]=prevent;duringDragEvents["dragstart"]=prevent;duringDragEvents["touchmove mousemove"]=move;duringDragEvents["touchend mouseup"]=stop;function prevent(e){if(e.stopPropagation)e.stopPropagation();
if(e.preventDefault)e.preventDefault();e.returnValue=false}function move(e){if(dragging){if(IE&&doc.documentMode<9&&!e.button)return stop();var t0=e.originalEvent&&e.originalEvent.touches&&e.originalEvent.touches[0];var pageX=t0&&t0.pageX||e.pageX;var pageY=t0&&t0.pageY||e.pageY;var dragX=Math.max(0,Math.min(pageX-offset.left,maxWidth));var dragY=Math.max(0,Math.min(pageY-offset.top,maxHeight));if(hasTouch)prevent(e);onmove.apply(element,[dragX,dragY,e])}}function start(e){var rightclick=e.which?
e.which==3:e.button==2;if(!rightclick&&!dragging)if(onstart.apply(element,arguments)!==false){dragging=true;maxHeight=$(element).height();maxWidth=$(element).width();offset=$(element).offset();$(doc).bind(duringDragEvents);$(doc.body).addClass("sp-dragging");move(e);prevent(e)}}function stop(){if(dragging){$(doc).unbind(duringDragEvents);$(doc.body).removeClass("sp-dragging");setTimeout(function(){onstop.apply(element,arguments)},0)}dragging=false}$(element).bind("touchstart mousedown",start)}function throttle(func,
wait,debounce){var timeout;return function(){var context=this,args=arguments;var throttler=function(){timeout=null;func.apply(context,args)};if(debounce)clearTimeout(timeout);if(debounce||!timeout)timeout=setTimeout(throttler,wait)}}function inputTypeColorSupport(){return $.fn.spectrum.inputTypeColorSupport()}var dataID="spectrum.id";$.fn.spectrum=function(opts,extra){if(typeof opts=="string"){var returnValue=this;var args=Array.prototype.slice.call(arguments,1);this.each(function(){var spect=spectrums[$(this).data(dataID)];
if(spect){var method=spect[opts];if(!method)throw new Error("Spectrum: no such method: '"+opts+"'");if(opts=="get")returnValue=spect.get();else if(opts=="container")returnValue=spect.container;else if(opts=="option")returnValue=spect.option.apply(spect,args);else if(opts=="destroy"){spect.destroy();$(this).removeData(dataID)}else method.apply(spect,args)}});return returnValue}return this.spectrum("destroy").each(function(){var options=$.extend({},opts,$(this).data());var spect=spectrum(this,options);
$(this).data(dataID,spect.id)})};$.fn.spectrum.load=true;$.fn.spectrum.loadOpts={};$.fn.spectrum.draggable=draggable;$.fn.spectrum.defaults=defaultOpts;$.fn.spectrum.inputTypeColorSupport=function inputTypeColorSupport(){if(typeof inputTypeColorSupport._cachedResult==="undefined"){var colorInput=$("<input type='color'/>")[0];inputTypeColorSupport._cachedResult=colorInput.type==="color"&&colorInput.value!==""}return inputTypeColorSupport._cachedResult};$.spectrum={};$.spectrum.localization={};$.spectrum.palettes=
{};$.fn.spectrum.processNativeColorInputs=function(){var colorInputs=$("input[type=color]");if(colorInputs.length&&!inputTypeColorSupport())colorInputs.spectrum({preferredFormat:"hex6"})};(function(){var trimLeft=/^[\s,#]+/,trimRight=/\s+$/,tinyCounter=0,math=Math,mathRound=math.round,mathMin=math.min,mathMax=math.max,mathRandom=math.random;var tinycolor=function(color,opts){color=color?color:"";opts=opts||{};if(color instanceof tinycolor)return color;if(!(this instanceof tinycolor))return new tinycolor(color,
opts);var rgb=inputToRGB(color);this._originalInput=color,this._r=rgb.r,this._g=rgb.g,this._b=rgb.b,this._a=rgb.a,this._roundA=mathRound(100*this._a)/100,this._format=opts.format||rgb.format;this._gradientType=opts.gradientType;if(this._r<1)this._r=mathRound(this._r);if(this._g<1)this._g=mathRound(this._g);if(this._b<1)this._b=mathRound(this._b);this._ok=rgb.ok;this._tc_id=tinyCounter++};tinycolor.prototype={isDark:function(){return this.getBrightness()<128},isLight:function(){return!this.isDark()},
isValid:function(){return this._ok},getOriginalInput:function(){return this._originalInput},getFormat:function(){return this._format},getAlpha:function(){return this._a},getBrightness:function(){var rgb=this.toRgb();return(rgb.r*299+rgb.g*587+rgb.b*114)/1E3},setAlpha:function(value){this._a=boundAlpha(value);this._roundA=mathRound(100*this._a)/100;return this},toHsv:function(){var hsv=rgbToHsv(this._r,this._g,this._b);return{h:hsv.h*360,s:hsv.s,v:hsv.v,a:this._a}},toHsvString:function(){var hsv=rgbToHsv(this._r,
this._g,this._b);var h=mathRound(hsv.h*360),s=mathRound(hsv.s*100),v=mathRound(hsv.v*100);return this._a==1?"hsv("+h+", "+s+"%, "+v+"%)":"hsva("+h+", "+s+"%, "+v+"%, "+this._roundA+")"},toHsl:function(){var hsl=rgbToHsl(this._r,this._g,this._b);return{h:hsl.h*360,s:hsl.s,l:hsl.l,a:this._a}},toHslString:function(){var hsl=rgbToHsl(this._r,this._g,this._b);var h=mathRound(hsl.h*360),s=mathRound(hsl.s*100),l=mathRound(hsl.l*100);return this._a==1?"hsl("+h+", "+s+"%, "+l+"%)":"hsla("+h+", "+s+"%, "+l+
"%, "+this._roundA+")"},toHex:function(allow3Char){return rgbToHex(this._r,this._g,this._b,allow3Char)},toHexString:function(allow3Char){return"#"+this.toHex(allow3Char)},toHex8:function(){return rgbaToHex(this._r,this._g,this._b,this._a)},toHex8String:function(){return"#"+this.toHex8()},toRgb:function(){return{r:mathRound(this._r),g:mathRound(this._g),b:mathRound(this._b),a:this._a}},toRgbString:function(){return this._a==1?"rgb("+mathRound(this._r)+", "+mathRound(this._g)+", "+mathRound(this._b)+
")":"rgba("+mathRound(this._r)+", "+mathRound(this._g)+", "+mathRound(this._b)+", "+this._roundA+")"},toPercentageRgb:function(){return{r:mathRound(bound01(this._r,255)*100)+"%",g:mathRound(bound01(this._g,255)*100)+"%",b:mathRound(bound01(this._b,255)*100)+"%",a:this._a}},toPercentageRgbString:function(){return this._a==1?"rgb("+mathRound(bound01(this._r,255)*100)+"%, "+mathRound(bound01(this._g,255)*100)+"%, "+mathRound(bound01(this._b,255)*100)+"%)":"rgba("+mathRound(bound01(this._r,255)*100)+
"%, "+mathRound(bound01(this._g,255)*100)+"%, "+mathRound(bound01(this._b,255)*100)+"%, "+this._roundA+")"},toName:function(){if(this._a===0)return"transparent";if(this._a<1)return false;return hexNames[rgbToHex(this._r,this._g,this._b,true)]||false},toFilter:function(secondColor){var hex8String="#"+rgbaToHex(this._r,this._g,this._b,this._a);var secondHex8String=hex8String;var gradientType=this._gradientType?"GradientType = 1, ":"";if(secondColor){var s=tinycolor(secondColor);secondHex8String=s.toHex8String()}return"progid:DXImageTransform.Microsoft.gradient("+
gradientType+"startColorstr="+hex8String+",endColorstr="+secondHex8String+")"},toString:function(format){var formatSet=!!format;format=format||this._format;var formattedString=false;var hasAlpha=this._a<1&&this._a>=0;var needsAlphaFormat=!formatSet&&hasAlpha&&(format==="hex"||format==="hex6"||format==="hex3"||format==="name");if(needsAlphaFormat){if(format==="name"&&this._a===0)return this.toName();return this.toRgbString()}if(format==="rgb")formattedString=this.toRgbString();if(format==="prgb")formattedString=
this.toPercentageRgbString();if(format==="hex"||format==="hex6")formattedString=this.toHexString();if(format==="hex3")formattedString=this.toHexString(true);if(format==="hex8")formattedString=this.toHex8String();if(format==="name")formattedString=this.toName();if(format==="hsl")formattedString=this.toHslString();if(format==="hsv")formattedString=this.toHsvString();return formattedString||this.toHexString()},_applyModification:function(fn,args){var color=fn.apply(null,[this].concat([].slice.call(args)));
this._r=color._r;this._g=color._g;this._b=color._b;this.setAlpha(color._a);return this},lighten:function(){return this._applyModification(lighten,arguments)},brighten:function(){return this._applyModification(brighten,arguments)},darken:function(){return this._applyModification(darken,arguments)},desaturate:function(){return this._applyModification(desaturate,arguments)},saturate:function(){return this._applyModification(saturate,arguments)},greyscale:function(){return this._applyModification(greyscale,
arguments)},spin:function(){return this._applyModification(spin,arguments)},_applyCombination:function(fn,args){return fn.apply(null,[this].concat([].slice.call(args)))},analogous:function(){return this._applyCombination(analogous,arguments)},complement:function(){return this._applyCombination(complement,arguments)},monochromatic:function(){return this._applyCombination(monochromatic,arguments)},splitcomplement:function(){return this._applyCombination(splitcomplement,arguments)},triad:function(){return this._applyCombination(triad,
arguments)},tetrad:function(){return this._applyCombination(tetrad,arguments)}};tinycolor.fromRatio=function(color,opts){if(typeof color=="object"){var newColor={};for(var i in color)if(color.hasOwnProperty(i))if(i==="a")newColor[i]=color[i];else newColor[i]=convertToPercentage(color[i]);color=newColor}return tinycolor(color,opts)};function inputToRGB(color){var rgb={r:0,g:0,b:0};var a=1;var ok=false;var format=false;if(typeof color=="string")color=stringInputToObject(color);if(typeof color=="object"){if(color.hasOwnProperty("r")&&
color.hasOwnProperty("g")&&color.hasOwnProperty("b")){rgb=rgbToRgb(color.r,color.g,color.b);ok=true;format=String(color.r).substr(-1)==="%"?"prgb":"rgb"}else if(color.hasOwnProperty("h")&&color.hasOwnProperty("s")&&color.hasOwnProperty("v")){color.s=convertToPercentage(color.s);color.v=convertToPercentage(color.v);rgb=hsvToRgb(color.h,color.s,color.v);ok=true;format="hsv"}else if(color.hasOwnProperty("h")&&color.hasOwnProperty("s")&&color.hasOwnProperty("l")){color.s=convertToPercentage(color.s);
color.l=convertToPercentage(color.l);rgb=hslToRgb(color.h,color.s,color.l);ok=true;format="hsl"}if(color.hasOwnProperty("a"))a=color.a}a=boundAlpha(a);return{ok:ok,format:color.format||format,r:mathMin(255,mathMax(rgb.r,0)),g:mathMin(255,mathMax(rgb.g,0)),b:mathMin(255,mathMax(rgb.b,0)),a:a}}function rgbToRgb(r,g,b){return{r:bound01(r,255)*255,g:bound01(g,255)*255,b:bound01(b,255)*255}}function rgbToHsl(r,g,b){r=bound01(r,255);g=bound01(g,255);b=bound01(b,255);var max=mathMax(r,g,b),min=mathMin(r,
g,b);var h,s,l=(max+min)/2;if(max==min)h=s=0;else{var d=max-min;s=l>.5?d/(2-max-min):d/(max+min);switch(max){case r:h=(g-b)/d+(g<b?6:0);break;case g:h=(b-r)/d+2;break;case b:h=(r-g)/d+4;break}h/=6}return{h:h,s:s,l:l}}function hslToRgb(h,s,l){var r,g,b;h=bound01(h,360);s=bound01(s,100);l=bound01(l,100);function hue2rgb(p,q,t){if(t<0)t+=1;if(t>1)t-=1;if(t<1/6)return p+(q-p)*6*t;if(t<1/2)return q;if(t<2/3)return p+(q-p)*(2/3-t)*6;return p}if(s===0)r=g=b=l;else{var q=l<.5?l*(1+s):l+s-l*s;var p=2*l-q;
r=hue2rgb(p,q,h+1/3);g=hue2rgb(p,q,h);b=hue2rgb(p,q,h-1/3)}return{r:r*255,g:g*255,b:b*255}}function rgbToHsv(r,g,b){r=bound01(r,255);g=bound01(g,255);b=bound01(b,255);var max=mathMax(r,g,b),min=mathMin(r,g,b);var h,s,v=max;var d=max-min;s=max===0?0:d/max;if(max==min)h=0;else{switch(max){case r:h=(g-b)/d+(g<b?6:0);break;case g:h=(b-r)/d+2;break;case b:h=(r-g)/d+4;break}h/=6}return{h:h,s:s,v:v}}function hsvToRgb(h,s,v){h=bound01(h,360)*6;s=bound01(s,100);v=bound01(v,100);var i=math.floor(h),f=h-i,p=
v*(1-s),q=v*(1-f*s),t=v*(1-(1-f)*s),mod=i%6,r=[v,q,p,p,t,v][mod],g=[t,v,v,q,p,p][mod],b=[p,p,t,v,v,q][mod];return{r:r*255,g:g*255,b:b*255}}function rgbToHex(r,g,b,allow3Char){var hex=[pad2(mathRound(r).toString(16)),pad2(mathRound(g).toString(16)),pad2(mathRound(b).toString(16))];if(allow3Char&&hex[0].charAt(0)==hex[0].charAt(1)&&hex[1].charAt(0)==hex[1].charAt(1)&&hex[2].charAt(0)==hex[2].charAt(1))return hex[0].charAt(0)+hex[1].charAt(0)+hex[2].charAt(0);return hex.join("")}function rgbaToHex(r,
g,b,a){var hex=[pad2(convertDecimalToHex(a)),pad2(mathRound(r).toString(16)),pad2(mathRound(g).toString(16)),pad2(mathRound(b).toString(16))];return hex.join("")}tinycolor.equals=function(color1,color2){if(!color1||!color2)return false;return tinycolor(color1).toRgbString()==tinycolor(color2).toRgbString()};tinycolor.random=function(){return tinycolor.fromRatio({r:mathRandom(),g:mathRandom(),b:mathRandom()})};function desaturate(color,amount){amount=amount===0?0:amount||10;var hsl=tinycolor(color).toHsl();
hsl.s-=amount/100;hsl.s=clamp01(hsl.s);return tinycolor(hsl)}function saturate(color,amount){amount=amount===0?0:amount||10;var hsl=tinycolor(color).toHsl();hsl.s+=amount/100;hsl.s=clamp01(hsl.s);return tinycolor(hsl)}function greyscale(color){return tinycolor(color).desaturate(100)}function lighten(color,amount){amount=amount===0?0:amount||10;var hsl=tinycolor(color).toHsl();hsl.l+=amount/100;hsl.l=clamp01(hsl.l);return tinycolor(hsl)}function brighten(color,amount){amount=amount===0?0:amount||10;
var rgb=tinycolor(color).toRgb();rgb.r=mathMax(0,mathMin(255,rgb.r-mathRound(255*-(amount/100))));rgb.g=mathMax(0,mathMin(255,rgb.g-mathRound(255*-(amount/100))));rgb.b=mathMax(0,mathMin(255,rgb.b-mathRound(255*-(amount/100))));return tinycolor(rgb)}function darken(color,amount){amount=amount===0?0:amount||10;var hsl=tinycolor(color).toHsl();hsl.l-=amount/100;hsl.l=clamp01(hsl.l);return tinycolor(hsl)}function spin(color,amount){var hsl=tinycolor(color).toHsl();var hue=(mathRound(hsl.h)+amount)%360;
hsl.h=hue<0?360+hue:hue;return tinycolor(hsl)}function complement(color){var hsl=tinycolor(color).toHsl();hsl.h=(hsl.h+180)%360;return tinycolor(hsl)}function triad(color){var hsl=tinycolor(color).toHsl();var h=hsl.h;return[tinycolor(color),tinycolor({h:(h+120)%360,s:hsl.s,l:hsl.l}),tinycolor({h:(h+240)%360,s:hsl.s,l:hsl.l})]}function tetrad(color){var hsl=tinycolor(color).toHsl();var h=hsl.h;return[tinycolor(color),tinycolor({h:(h+90)%360,s:hsl.s,l:hsl.l}),tinycolor({h:(h+180)%360,s:hsl.s,l:hsl.l}),
tinycolor({h:(h+270)%360,s:hsl.s,l:hsl.l})]}function splitcomplement(color){var hsl=tinycolor(color).toHsl();var h=hsl.h;return[tinycolor(color),tinycolor({h:(h+72)%360,s:hsl.s,l:hsl.l}),tinycolor({h:(h+216)%360,s:hsl.s,l:hsl.l})]}function analogous(color,results,slices){results=results||6;slices=slices||30;var hsl=tinycolor(color).toHsl();var part=360/slices;var ret=[tinycolor(color)];for(hsl.h=(hsl.h-(part*results>>1)+720)%360;--results;){hsl.h=(hsl.h+part)%360;ret.push(tinycolor(hsl))}return ret}
function monochromatic(color,results){results=results||6;var hsv=tinycolor(color).toHsv();var h=hsv.h,s=hsv.s,v=hsv.v;var ret=[];var modification=1/results;while(results--){ret.push(tinycolor({h:h,s:s,v:v}));v=(v+modification)%1}return ret}tinycolor.mix=function(color1,color2,amount){amount=amount===0?0:amount||50;var rgb1=tinycolor(color1).toRgb();var rgb2=tinycolor(color2).toRgb();var p=amount/100;var w=p*2-1;var a=rgb2.a-rgb1.a;var w1;if(w*a==-1)w1=w;else w1=(w+a)/(1+w*a);w1=(w1+1)/2;var w2=1-
w1;var rgba={r:rgb2.r*w1+rgb1.r*w2,g:rgb2.g*w1+rgb1.g*w2,b:rgb2.b*w1+rgb1.b*w2,a:rgb2.a*p+rgb1.a*(1-p)};return tinycolor(rgba)};tinycolor.readability=function(color1,color2){var c1=tinycolor(color1);var c2=tinycolor(color2);var rgb1=c1.toRgb();var rgb2=c2.toRgb();var brightnessA=c1.getBrightness();var brightnessB=c2.getBrightness();var colorDiff=Math.max(rgb1.r,rgb2.r)-Math.min(rgb1.r,rgb2.r)+Math.max(rgb1.g,rgb2.g)-Math.min(rgb1.g,rgb2.g)+Math.max(rgb1.b,rgb2.b)-Math.min(rgb1.b,rgb2.b);return{brightness:Math.abs(brightnessA-
brightnessB),color:colorDiff}};tinycolor.isReadable=function(color1,color2){var readability=tinycolor.readability(color1,color2);return readability.brightness>125&&readability.color>500};tinycolor.mostReadable=function(baseColor,colorList){var bestColor=null;var bestScore=0;var bestIsReadable=false;for(var i=0;i<colorList.length;i++){var readability=tinycolor.readability(baseColor,colorList[i]);var readable=readability.brightness>125&&readability.color>500;var score=3*(readability.brightness/125)+
readability.color/500;if(readable&&!bestIsReadable||readable&&bestIsReadable&&score>bestScore||!readable&&!bestIsReadable&&score>bestScore){bestIsReadable=readable;bestScore=score;bestColor=tinycolor(colorList[i])}}return bestColor};var names=tinycolor.names={aliceblue:"f0f8ff",antiquewhite:"faebd7",aqua:"0ff",aquamarine:"7fffd4",azure:"f0ffff",beige:"f5f5dc",bisque:"ffe4c4",black:"000",blanchedalmond:"ffebcd",blue:"00f",blueviolet:"8a2be2",brown:"a52a2a",burlywood:"deb887",burntsienna:"ea7e5d",cadetblue:"5f9ea0",
chartreuse:"7fff00",chocolate:"d2691e",coral:"ff7f50",cornflowerblue:"6495ed",cornsilk:"fff8dc",crimson:"dc143c",cyan:"0ff",darkblue:"00008b",darkcyan:"008b8b",darkgoldenrod:"b8860b",darkgray:"a9a9a9",darkgreen:"006400",darkgrey:"a9a9a9",darkkhaki:"bdb76b",darkmagenta:"8b008b",darkolivegreen:"556b2f",darkorange:"ff8c00",darkorchid:"9932cc",darkred:"8b0000",darksalmon:"e9967a",darkseagreen:"8fbc8f",darkslateblue:"483d8b",darkslategray:"2f4f4f",darkslategrey:"2f4f4f",darkturquoise:"00ced1",darkviolet:"9400d3",
deeppink:"ff1493",deepskyblue:"00bfff",dimgray:"696969",dimgrey:"696969",dodgerblue:"1e90ff",firebrick:"b22222",floralwhite:"fffaf0",forestgreen:"228b22",fuchsia:"f0f",gainsboro:"dcdcdc",ghostwhite:"f8f8ff",gold:"ffd700",goldenrod:"daa520",gray:"808080",green:"008000",greenyellow:"adff2f",grey:"808080",honeydew:"f0fff0",hotpink:"ff69b4",indianred:"cd5c5c",indigo:"4b0082",ivory:"fffff0",khaki:"f0e68c",lavender:"e6e6fa",lavenderblush:"fff0f5",lawngreen:"7cfc00",lemonchiffon:"fffacd",lightblue:"add8e6",
lightcoral:"f08080",lightcyan:"e0ffff",lightgoldenrodyellow:"fafad2",lightgray:"d3d3d3",lightgreen:"90ee90",lightgrey:"d3d3d3",lightpink:"ffb6c1",lightsalmon:"ffa07a",lightseagreen:"20b2aa",lightskyblue:"87cefa",lightslategray:"789",lightslategrey:"789",lightsteelblue:"b0c4de",lightyellow:"ffffe0",lime:"0f0",limegreen:"32cd32",linen:"faf0e6",magenta:"f0f",maroon:"800000",mediumaquamarine:"66cdaa",mediumblue:"0000cd",mediumorchid:"ba55d3",mediumpurple:"9370db",mediumseagreen:"3cb371",mediumslateblue:"7b68ee",
mediumspringgreen:"00fa9a",mediumturquoise:"48d1cc",mediumvioletred:"c71585",midnightblue:"191970",mintcream:"f5fffa",mistyrose:"ffe4e1",moccasin:"ffe4b5",navajowhite:"ffdead",navy:"000080",oldlace:"fdf5e6",olive:"808000",olivedrab:"6b8e23",orange:"ffa500",orangered:"ff4500",orchid:"da70d6",palegoldenrod:"eee8aa",palegreen:"98fb98",paleturquoise:"afeeee",palevioletred:"db7093",papayawhip:"ffefd5",peachpuff:"ffdab9",peru:"cd853f",pink:"ffc0cb",plum:"dda0dd",powderblue:"b0e0e6",purple:"800080",rebeccapurple:"663399",
red:"f00",rosybrown:"bc8f8f",royalblue:"4169e1",saddlebrown:"8b4513",salmon:"fa8072",sandybrown:"f4a460",seagreen:"2e8b57",seashell:"fff5ee",sienna:"a0522d",silver:"c0c0c0",skyblue:"87ceeb",slateblue:"6a5acd",slategray:"708090",slategrey:"708090",snow:"fffafa",springgreen:"00ff7f",steelblue:"4682b4",tan:"d2b48c",teal:"008080",thistle:"d8bfd8",tomato:"ff6347",turquoise:"40e0d0",violet:"ee82ee",wheat:"f5deb3",white:"fff",whitesmoke:"f5f5f5",yellow:"ff0",yellowgreen:"9acd32"};var hexNames=tinycolor.hexNames=
flip(names);function flip(o){var flipped={};for(var i in o)if(o.hasOwnProperty(i))flipped[o[i]]=i;return flipped}function boundAlpha(a){a=parseFloat(a);if(isNaN(a)||a<0||a>1)a=1;return a}function bound01(n,max){if(isOnePointZero(n))n="100%";var processPercent=isPercentage(n);n=mathMin(max,mathMax(0,parseFloat(n)));if(processPercent)n=parseInt(n*max,10)/100;if(math.abs(n-max)<1E-6)return 1;return n%max/parseFloat(max)}function clamp01(val){return mathMin(1,mathMax(0,val))}function parseIntFromHex(val){return parseInt(val,
16)}function isOnePointZero(n){return typeof n=="string"&&n.indexOf(".")!=-1&&parseFloat(n)===1}function isPercentage(n){return typeof n==="string"&&n.indexOf("%")!=-1}function pad2(c){return c.length==1?"0"+c:""+c}function convertToPercentage(n){if(n<=1)n=n*100+"%";return n}function convertDecimalToHex(d){return Math.round(parseFloat(d)*255).toString(16)}function convertHexToDecimal(h){return parseIntFromHex(h)/255}var matchers=function(){var CSS_INTEGER="[-\\+]?\\d+%?";var CSS_NUMBER="[-\\+]?\\d*\\.\\d+%?";
var CSS_UNIT="(?:"+CSS_NUMBER+")|(?:"+CSS_INTEGER+")";var PERMISSIVE_MATCH3="[\\s|\\(]+("+CSS_UNIT+")[,|\\s]+("+CSS_UNIT+")[,|\\s]+("+CSS_UNIT+")\\s*\\)?";var PERMISSIVE_MATCH4="[\\s|\\(]+("+CSS_UNIT+")[,|\\s]+("+CSS_UNIT+")[,|\\s]+("+CSS_UNIT+")[,|\\s]+("+CSS_UNIT+")\\s*\\)?";return{rgb:new RegExp("rgb"+PERMISSIVE_MATCH3),rgba:new RegExp("rgba"+PERMISSIVE_MATCH4),hsl:new RegExp("hsl"+PERMISSIVE_MATCH3),hsla:new RegExp("hsla"+PERMISSIVE_MATCH4),hsv:new RegExp("hsv"+PERMISSIVE_MATCH3),hsva:new RegExp("hsva"+
PERMISSIVE_MATCH4),hex3:/^([0-9a-fA-F]{1})([0-9a-fA-F]{1})([0-9a-fA-F]{1})$/,hex6:/^([0-9a-fA-F]{2})([0-9a-fA-F]{2})([0-9a-fA-F]{2})$/,hex8:/^([0-9a-fA-F]{2})([0-9a-fA-F]{2})([0-9a-fA-F]{2})([0-9a-fA-F]{2})$/}}();function stringInputToObject(color){color=color.replace(trimLeft,"").replace(trimRight,"").toLowerCase();var named=false;if(names[color]){color=names[color];named=true}else if(color=="transparent")return{r:0,g:0,b:0,a:0,format:"name"};var match;if(match=matchers.rgb.exec(color))return{r:match[1],
g:match[2],b:match[3]};if(match=matchers.rgba.exec(color))return{r:match[1],g:match[2],b:match[3],a:match[4]};if(match=matchers.hsl.exec(color))return{h:match[1],s:match[2],l:match[3]};if(match=matchers.hsla.exec(color))return{h:match[1],s:match[2],l:match[3],a:match[4]};if(match=matchers.hsv.exec(color))return{h:match[1],s:match[2],v:match[3]};if(match=matchers.hsva.exec(color))return{h:match[1],s:match[2],v:match[3],a:match[4]};if(match=matchers.hex8.exec(color))return{a:convertHexToDecimal(match[1]),
r:parseIntFromHex(match[2]),g:parseIntFromHex(match[3]),b:parseIntFromHex(match[4]),format:named?"name":"hex8"};if(match=matchers.hex6.exec(color))return{r:parseIntFromHex(match[1]),g:parseIntFromHex(match[2]),b:parseIntFromHex(match[3]),format:named?"name":"hex"};if(match=matchers.hex3.exec(color))return{r:parseIntFromHex(match[1]+""+match[1]),g:parseIntFromHex(match[2]+""+match[2]),b:parseIntFromHex(match[3]+""+match[3]),format:named?"name":"hex"};return false}window.tinycolor=tinycolor})();$(function(){if($.fn.spectrum.load)$.fn.spectrum.processNativeColorInputs()})});


/*!
 * clipboard.js v1.5.10
 * https://zenorocha.github.io/clipboard.js
 * https://github.com/zenorocha/clipboard.js/
 *
 * Licensed MIT © Zeno Rocha
 */
!function(t){if("object"==typeof exports&&"undefined"!=typeof module)module.exports=t();else if("function"==typeof define&&define.amd)define([],t);else{var e;e="undefined"!=typeof window?window:"undefined"!=typeof global?global:"undefined"!=typeof self?self:this,e.Clipboard=t()}}(function(){var t,e,n;return function t(e,n,o){function i(c,a){if(!n[c]){if(!e[c]){var s="function"==typeof require&&require;if(!a&&s)return s(c,!0);if(r)return r(c,!0);var l=new Error("Cannot find module '"+c+"'");throw l.code="MODULE_NOT_FOUND",l}var u=n[c]={exports:{}};e[c][0].call(u.exports,function(t){var n=e[c][1][t];return i(n?n:t)},u,u.exports,t,e,n,o)}return n[c].exports}for(var r="function"==typeof require&&require,c=0;c<o.length;c++)i(o[c]);return i}({1:[function(t,e,n){var o=t("matches-selector");e.exports=function(t,e,n){for(var i=n?t:t.parentNode;i&&i!==document;){if(o(i,e))return i;i=i.parentNode}}},{"matches-selector":5}],2:[function(t,e,n){function o(t,e,n,o,r){var c=i.apply(this,arguments);return t.addEventListener(n,c,r),{destroy:function(){t.removeEventListener(n,c,r)}}}function i(t,e,n,o){return function(n){n.delegateTarget=r(n.target,e,!0),n.delegateTarget&&o.call(t,n)}}var r=t("closest");e.exports=o},{closest:1}],3:[function(t,e,n){n.node=function(t){return void 0!==t&&t instanceof HTMLElement&&1===t.nodeType},n.nodeList=function(t){var e=Object.prototype.toString.call(t);return void 0!==t&&("[object NodeList]"===e||"[object HTMLCollection]"===e)&&"length"in t&&(0===t.length||n.node(t[0]))},n.string=function(t){return"string"==typeof t||t instanceof String},n.fn=function(t){var e=Object.prototype.toString.call(t);return"[object Function]"===e}},{}],4:[function(t,e,n){function o(t,e,n){if(!t&&!e&&!n)throw new Error("Missing required arguments");if(!a.string(e))throw new TypeError("Second argument must be a String");if(!a.fn(n))throw new TypeError("Third argument must be a Function");if(a.node(t))return i(t,e,n);if(a.nodeList(t))return r(t,e,n);if(a.string(t))return c(t,e,n);throw new TypeError("First argument must be a String, HTMLElement, HTMLCollection, or NodeList")}function i(t,e,n){return t.addEventListener(e,n),{destroy:function(){t.removeEventListener(e,n)}}}function r(t,e,n){return Array.prototype.forEach.call(t,function(t){t.addEventListener(e,n)}),{destroy:function(){Array.prototype.forEach.call(t,function(t){t.removeEventListener(e,n)})}}}function c(t,e,n){return s(document.body,t,e,n)}var a=t("./is"),s=t("delegate");e.exports=o},{"./is":3,delegate:2}],5:[function(t,e,n){function o(t,e){if(r)return r.call(t,e);for(var n=t.parentNode.querySelectorAll(e),o=0;o<n.length;++o)if(n[o]==t)return!0;return!1}var i=Element.prototype,r=i.matchesSelector||i.webkitMatchesSelector||i.mozMatchesSelector||i.msMatchesSelector||i.oMatchesSelector;e.exports=o},{}],6:[function(t,e,n){function o(t){var e;if("INPUT"===t.nodeName||"TEXTAREA"===t.nodeName)t.focus(),t.setSelectionRange(0,t.value.length),e=t.value;else{t.hasAttribute("contenteditable")&&t.focus();var n=window.getSelection(),o=document.createRange();o.selectNodeContents(t),n.removeAllRanges(),n.addRange(o),e=n.toString()}return e}e.exports=o},{}],7:[function(t,e,n){function o(){}o.prototype={on:function(t,e,n){var o=this.e||(this.e={});return(o[t]||(o[t]=[])).push({fn:e,ctx:n}),this},once:function(t,e,n){function o(){i.off(t,o),e.apply(n,arguments)}var i=this;return o._=e,this.on(t,o,n)},emit:function(t){var e=[].slice.call(arguments,1),n=((this.e||(this.e={}))[t]||[]).slice(),o=0,i=n.length;for(o;i>o;o++)n[o].fn.apply(n[o].ctx,e);return this},off:function(t,e){var n=this.e||(this.e={}),o=n[t],i=[];if(o&&e)for(var r=0,c=o.length;c>r;r++)o[r].fn!==e&&o[r].fn._!==e&&i.push(o[r]);return i.length?n[t]=i:delete n[t],this}},e.exports=o},{}],8:[function(e,n,o){!function(i,r){if("function"==typeof t&&t.amd)t(["module","select"],r);else if("undefined"!=typeof o)r(n,e("select"));else{var c={exports:{}};r(c,i.select),i.clipboardAction=c.exports}}(this,function(t,e){"use strict";function n(t){return t&&t.__esModule?t:{"default":t}}function o(t,e){if(!(t instanceof e))throw new TypeError("Cannot call a class as a function")}var i=n(e),r="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(t){return typeof t}:function(t){return t&&"function"==typeof Symbol&&t.constructor===Symbol?"symbol":typeof t},c=function(){function t(t,e){for(var n=0;n<e.length;n++){var o=e[n];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(t,o.key,o)}}return function(e,n,o){return n&&t(e.prototype,n),o&&t(e,o),e}}(),a=function(){function t(e){o(this,t),this.resolveOptions(e),this.initSelection()}return t.prototype.resolveOptions=function t(){var e=arguments.length<=0||void 0===arguments[0]?{}:arguments[0];this.action=e.action,this.emitter=e.emitter,this.target=e.target,this.text=e.text,this.trigger=e.trigger,this.selectedText=""},t.prototype.initSelection=function t(){this.text?this.selectFake():this.target&&this.selectTarget()},t.prototype.selectFake=function t(){var e=this,n="rtl"==document.documentElement.getAttribute("dir");this.removeFake(),this.fakeHandler=document.body.addEventListener("click",function(){return e.removeFake()}),this.fakeElem=document.createElement("textarea"),this.fakeElem.style.fontSize="12pt",this.fakeElem.style.border="0",this.fakeElem.style.padding="0",this.fakeElem.style.margin="0",this.fakeElem.style.position="fixed",this.fakeElem.style[n?"right":"left"]="-9999px",this.fakeElem.style.top=(window.pageYOffset||document.documentElement.scrollTop)+"px",this.fakeElem.setAttribute("readonly",""),this.fakeElem.value=this.text,document.body.appendChild(this.fakeElem),this.selectedText=(0,i.default)(this.fakeElem),this.copyText()},t.prototype.removeFake=function t(){this.fakeHandler&&(document.body.removeEventListener("click"),this.fakeHandler=null),this.fakeElem&&(document.body.removeChild(this.fakeElem),this.fakeElem=null)},t.prototype.selectTarget=function t(){this.selectedText=(0,i.default)(this.target),this.copyText()},t.prototype.copyText=function t(){var e=void 0;try{e=document.execCommand(this.action)}catch(n){e=!1}this.handleResult(e)},t.prototype.handleResult=function t(e){e?this.emitter.emit("success",{action:this.action,text:this.selectedText,trigger:this.trigger,clearSelection:this.clearSelection.bind(this)}):this.emitter.emit("error",{action:this.action,trigger:this.trigger,clearSelection:this.clearSelection.bind(this)})},t.prototype.clearSelection=function t(){this.target&&this.target.blur(),window.getSelection().removeAllRanges()},t.prototype.destroy=function t(){this.removeFake()},c(t,[{key:"action",set:function t(){var e=arguments.length<=0||void 0===arguments[0]?"copy":arguments[0];if(this._action=e,"copy"!==this._action&&"cut"!==this._action)throw new Error('Invalid "action" value, use either "copy" or "cut"')},get:function t(){return this._action}},{key:"target",set:function t(e){if(void 0!==e){if(!e||"object"!==("undefined"==typeof e?"undefined":r(e))||1!==e.nodeType)throw new Error('Invalid "target" value, use a valid Element');if("copy"===this.action&&e.hasAttribute("disabled"))throw new Error('Invalid "target" attribute. Please use "readonly" instead of "disabled" attribute');if("cut"===this.action&&(e.hasAttribute("readonly")||e.hasAttribute("disabled")))throw new Error('Invalid "target" attribute. You can\'t cut text from elements with "readonly" or "disabled" attributes');this._target=e}},get:function t(){return this._target}}]),t}();t.exports=a})},{select:6}],9:[function(e,n,o){!function(i,r){if("function"==typeof t&&t.amd)t(["module","./clipboard-action","tiny-emitter","good-listener"],r);else if("undefined"!=typeof o)r(n,e("./clipboard-action"),e("tiny-emitter"),e("good-listener"));else{var c={exports:{}};r(c,i.clipboardAction,i.tinyEmitter,i.goodListener),i.clipboard=c.exports}}(this,function(t,e,n,o){"use strict";function i(t){return t&&t.__esModule?t:{"default":t}}function r(t,e){if(!(t instanceof e))throw new TypeError("Cannot call a class as a function")}function c(t,e){if(!t)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!e||"object"!=typeof e&&"function"!=typeof e?t:e}function a(t,e){if("function"!=typeof e&&null!==e)throw new TypeError("Super expression must either be null or a function, not "+typeof e);t.prototype=Object.create(e&&e.prototype,{constructor:{value:t,enumerable:!1,writable:!0,configurable:!0}}),e&&(Object.setPrototypeOf?Object.setPrototypeOf(t,e):t.__proto__=e)}function s(t,e){var n="data-clipboard-"+t;if(e.hasAttribute(n))return e.getAttribute(n)}var l=i(e),u=i(n),f=i(o),d=function(t){function e(n,o){r(this,e);var i=c(this,t.call(this));return i.resolveOptions(o),i.listenClick(n),i}return a(e,t),e.prototype.resolveOptions=function t(){var e=arguments.length<=0||void 0===arguments[0]?{}:arguments[0];this.action="function"==typeof e.action?e.action:this.defaultAction,this.target="function"==typeof e.target?e.target:this.defaultTarget,this.text="function"==typeof e.text?e.text:this.defaultText},e.prototype.listenClick=function t(e){var n=this;this.listener=(0,f.default)(e,"click",function(t){return n.onClick(t)})},e.prototype.onClick=function t(e){var n=e.delegateTarget||e.currentTarget;this.clipboardAction&&(this.clipboardAction=null),this.clipboardAction=new l.default({action:this.action(n),target:this.target(n),text:this.text(n),trigger:n,emitter:this})},e.prototype.defaultAction=function t(e){return s("action",e)},e.prototype.defaultTarget=function t(e){var n=s("target",e);return n?document.querySelector(n):void 0},e.prototype.defaultText=function t(e){return s("text",e)},e.prototype.destroy=function t(){this.listener.destroy(),this.clipboardAction&&(this.clipboardAction.destroy(),this.clipboardAction=null)},e}(u.default);t.exports=d})},{"./clipboard-action":8,"good-listener":4,"tiny-emitter":7}]},{},[9])(9)});