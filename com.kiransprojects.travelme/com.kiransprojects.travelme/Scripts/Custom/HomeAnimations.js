$(document).ready(function ()
{
    /*
     * Removes hide class and adds show class
     */
    function addShowClass(object)
    {
        object.removeClass("hide").addClass("show");
    }

     var animation_speed = 1000; 

    ///Animate Title
    $("#title").fadeIn(animation_speed, function ()
    {
        addShowClass($(this)); 
    });

    ///Animate underline
    $("#title_underline").animate({ width: 'toggle' }, animation_speed * 1.5, function ()
    {
        addShowClass($(this));

        //icon 1
        $(".selling_point_node").fadeToggle(animation_speed / 2, function ()
        {
            addShowClass($(this));

            //icon 2
            $(".selling_point_node2").fadeToggle(animation_speed / 2, function ()
            {
                addShowClass($(this));

                //icon 3
                $(".selling_point_node3").fadeToggle(animation_speed / 2, function ()
                {
                    addShowClass($(this));
                });
            });
        });
    });
}); 