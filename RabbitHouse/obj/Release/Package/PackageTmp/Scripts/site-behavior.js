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
        var dialogOrgWidth = parseInt($this.find('.dialog').css('width').replace('px', ' '));

        var dialogValueBeforeControl
        update = function () {
                
                var singleDialogWidth = parseInt($this.css('width').replace('px', ' '));

                var avatorWidth = parseInt($this.find('.avator').css('width').replace('px', ' '));
                var avatorMarginRight = parseInt($this.find('.avator').css('margin-right').replace('px', ' '));
                var avatorMarginLeft = parseInt($this.find('.avator').css('margin-left').replace('px', ' '));
                var avatorMagrinValue = avatorMarginLeft > avatorMarginRight ? avatorMarginLeft : avatorMarginRight;
                
                $this.find('.dialog').css('max-width', singleDialogWidth - avatorWidth - avatorMagrinValue);
        };

        $(window).bind('resize', update);

        $(function () {
            update();
        });

    }
})(jQuery);

(function ($) {

    $.fn.modelSubmitBlock = function (counter) {
        var $this = $(this);
        var nowCounter = counter;
        var itemTemplate = $this.find('.single-model');

        $(function () {
            $this.find('#add-factor-button').click(function () {
                //$('<div class="single-model row mb10"><div class="col-sm-3 mb5">' +
                //'<select class="form-control" name="ArticleDialogs[' + counter + '].CharacterId">' +
                //'</select>' +
                //'</div>'+
                //'<div class="col-sm-12">'+
                //'<input class="form-control text-box single-line" name="ArticleDialogs[' + counter + '].Message" type="text" value="" placeholder="" />'+
                //'</div>'+
                //'</div>'
                //).appendTo($this);
                var newItem = itemTemplate.first().clone();

                //repalce code
                newItem.find('[name="ArticleDialogs[0].CharacterId"]').attr('name', 'ArticleDialogs[' + counter + '].CharacterId').find('option[value="1"]').attr("selected",true);
                newItem.find('[name="ArticleDialogs[0].Message"]').attr('name', 'ArticleDialogs[' + counter + '].Message').val('');

                newItem.appendTo($this.find(".model-area"));

                counter++;
                return false;
            });
            $this.find("#remove-factor-button").click(function () {
                if (counter > 1)
                {
                    $this.find(".single-model").last().remove();
                    counter--;
                }
                return false;
            });
        });

    }
})(jQuery);