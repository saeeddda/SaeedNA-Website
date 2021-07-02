jQuery(document).ready(function($){
	$('.lievo-color-picker').wpColorPicker({
		defaultColor: function () {
			return $(this).data('savedcolor');
		},
		mode: 'hsl',
		hide: true,
		width: 300,
		palettes: false
	});

	$('#lievo_admin_form').on('submit', function() {
		if ( $('#submit', '#lievo_admin_form').hasClass('lievo-btn-deactivate') ) {
			var c = confirm('Are you sure you want to deactivate this code?');
			return c;
		};
	});
		
	$('#sizeUnits').on('change', function () {
		var val = $(this).val();
		if ( val === 'px' ) {
			var form = $('#size').val();
			$('#size').attr({min:1, step:1});
			$('#size').val(Math.round(form));
		} else {
			$('#size').attr({min:0.1, step:0.1});
		};
	});
	$('#sizeUnits').change();

	$('#customStrokeWidth').on('focus', function () {
		$('#strokeWidth').prop('checked', true);
	});

	$('#customRotate').on('focus', function () {
		$('#rotate').prop('checked', true);
	});

	$('#colorsOnHoverHue').on('focus', function () {
		$('#colorsOnHover').prop('checked', true);
	});

	$('#colorsWhenMorphHue').on('focus', function () {
		$('#colorsWhenMorph').prop('checked', true);
	});

	$('#morphImageUrl').on('focus', function () {
		$('#morphImage').prop('checked', true);
	});

	$('#strokeWidthFactorOnHoverValue').on('focus', function () {
		$('#strokeWidthFactorOnHover').prop('checked', true);
	});

	$('#eventOnElem').on('focus', function () {
		$('#eventOn').prop('checked', true);
	});

	$('#customDuration').on('focus', function () {
		$('#duration').prop('checked', true);
	});

	$('#customRepeat').on('focus', function () {
		$('#repeat').prop('checked', true);
	});

	$('#customRepeatDelay').on('focus', function () {
		$('#repeatDelay').prop('checked', true);
	});

	$('#customDrawColor msg_success').on('focus', function () {
		$('#customValueDrawColor').prop('checked', true);
	});

	if (window.CodeMirror) {
		var custom_css = CodeMirror.fromTextArea(document.getElementById("lievo_custom_css"), {
			lineNumbers: true,
			autoCloseBrackets: true,
			styleActiveLine: true,
			matchBrackets: true,
			mode: 'css'
		});
		var custom_js = CodeMirror.fromTextArea(document.getElementById("lievo_custom_js"), {
			lineNumbers: true,
			autoCloseBrackets: true,
			styleActiveLine: true,
			matchBrackets: true,
			mode: 'javascript'
		});
	};

});
