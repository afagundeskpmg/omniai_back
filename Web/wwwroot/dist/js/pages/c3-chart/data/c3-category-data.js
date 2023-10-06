/*************************************************************************************/
// -->Template Name: Bootstrap Press Admin
// -->Author: Themedesigner
// -->Email: niravjoshi87@gmail.com
// -->File: c3_chart_JS
/*************************************************************************************/
$(function() {
    var o = c3.generate({
        bindto: "#category-data",
        size: { height: 400 },
        color: { pattern: ["#4fc3f7", "#00378A"] },
        data: {
            x: "x",
            columns: [
                ["x", "www.site1.com", "www.site2.com", "www.site3.com", "www.site4.com"],
                ["complete", 400, 200, 100, 40],
                ["remaining", 190, 100, 140, 90]
            ],
            groups: [
                ["complete", "remaining"]
            ],
            type: "bar"
        },
        axis: { x: { type: "category" } },
        grid: { y: { show: !0 } }
    });
   
});