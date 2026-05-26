/*<![CDATA[*/
$(function() {
    $('div.sc_menu').mousemove(function(e) {
        var ul = $(this).find('ul:first-child')
	       , ulPadding = 15;
        var divWidth = $(this).width();
        var lastLi = ul.find('li:last-child');
        
        //As images are loaded ul width increases,
        //so we recalculate it each time
        var ulWidth = lastLi[0].offsetLeft + lastLi.outerWidth() + ulPadding;
        var left = (e.pageX - $(this).offset().left) * (ulWidth - divWidth) / divWidth;
        $(this).scrollLeft(left);
    });
});
/*]]>*/