 function slider() {
            //show first image delay time for showing
            $(".slider #1").show("fade", 500);
            $(".slider #1").delay(3330).hide("slide", { direction: 'right' },500);

            var sc = $(".slider img").size();
            var count = 2;

            setInterval(

                function()
                {
                    $(".slider #" + count).show("slide", { direction: 'left' }, 500);
                    $(".slider #" + count).delay(3330).hide("slide", { direction: 'right' }, 500);

                    if(count == sc){
                       count = 1;    
                    }else{ 
                    count= count +1;        }

                },4330); // 3330 + 500 + 500


        }
