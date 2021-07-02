$(window).on("load", function () {

    var l = {
        chart: {
            height: 270,
            type: "line",
            stacked: !1,
            toolbar: {
                show: !1
            },
            sparkline: {
                enabled: !0
            }
        },
        colors: ["#5A8DEE", "#5A8DEE"],
        dataLabels: { 
            enabled: !1
        },
        stroke: {
            curve: "smooth",
            width: 2.5,
            dashArray: [0, 8]
        },
        fill: {
            type: "gradient",
            gradient: {
                inverseColors: !1,
                shade: "light",
                type: "vertical",
                gradientToColors: ["#E2ECFF", "#5A8DEE"],
                opacityFrom: .7,
                opacityTo: .55,
                stops: [0, 80, 100]
            }
        },
        series: [{
            name: "This Month",
            data: [165, 175, 162, 173, 160, 195, 160, 170, 160, 190, 180],
            type: "area"
        }, {
            name: "Last Months",
            data: [168, 168, 155, 178, 155, 170, 190, 160, 150, 170, 140],
            type: "line"
        }],
        xaxis: {
            offsetY: -50,
            categories: ["", 1, 2, 3, 4, 5, 6, 7, 8, 9, ""],
            axisBorder: {
                show: !1
            },
            axisTicks: {
                show: !1
            },
            labels: {
                show: !0,
                style: {
                    colors: "#828D99"
                }
            }
        },
        tooltip: {
            x: {
                show: !1
            }
        }
    };
    new ApexCharts(document.querySelector("#order-summary-chart"), l).render();

});