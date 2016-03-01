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

(function ($) {

    $.fn.singleDialog = function () {
        var $this = $(this);

        update = function () {
            var parentWidthSpace = parseInt($this.parent().css('width').replace('px', ' ')) - 2*parseInt($this.parent().css('padding-left').replace('px', ' '));          

            var avatorWidth = parseInt($this.find('.avator').css('width').replace('px', ' '));
            var avatorMarginRight = parseInt($this.find('.avator').css('margin-right').replace('px', ' '));

            $this.find('.dialog').css('width', parentWidthSpace - avatorWidth - avatorMarginRight);

            var dialogWidth = parseInt($this.find('.dialog').css('width').replace('px', ' '));
            var dialogPadding = parseInt($this.find('.dialog').css('padding-left').replace('px', ' '));
            var dialogPWidth = parseInt($this.find('p').css('width').replace('px', ' '));

            //alert(dialogWidth.toString());
            //alert(dialogPadding.toString());
            //alert(dialogPWidth.toString());
            if(dialogPWidth<=dialogWidth-2*dialogPadding)
            {
                $this.find('.dialog').css('width', dialogPWidth+2*dialogPadding);
            }
                
        };

        $(window).bind('resize', update);

        $(function () {
            update();
        });

    }
})(jQuery);