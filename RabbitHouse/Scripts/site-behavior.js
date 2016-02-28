(function ($) {

    $.fn.variableBlock = function () {
        var $this = $(this);

        update = function () {
            var fullWidth = parseInt($this.css('width').replace('px',' '));
            var fullHeight = parseInt($this.css('height').replace('px', ' '));
            var gap = 4;
            var resultWidth;
            var resultHeight;
            
            resultWidth = Math.floor(((fullWidth - gap * 2) * 3 / 8)).toString()+'px';
            resultHeight = Math.floor((fullHeight - gap)).toString() + 'px';
            $('#coffee-block').css({ width: resultWidth, height: resultHeight });

            resultWidth = Math.floor(((fullWidth - gap * 2) * 5 / 8)).toString() + 'px';
            resultHeight = Math.floor(((fullHeight - gap * 2) / 2)).toString() + 'px';
            $('#dessert-block').css({ width: resultWidth, height: resultHeight });

            resultWidth = Math.floor(((fullWidth - gap * 3 ) /4 )).toString() + 'px';
            resultHeight = Math.floor(((fullHeight - gap * 2) / 2)).toString() + 'px';
            $('#music-block').css({ width: resultWidth, height: resultHeight });

            resultWidth = Math.floor(((fullWidth - gap * 3) * 3/ 8)).toString() -1+ 'px';
            resultHeight = Math.floor(((fullHeight - gap * 2) / 2)).toString() + 'px';
            $('#drink-block').css({ width: resultWidth, height: resultHeight });


        };

        $(window).bind('resize', update);

        $(function () {
            update();
        });

    }
})(jQuery);