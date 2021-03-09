
$(function(){
    
    // preload audio
    var toast = new Audio('media/click.mp3');
    
$('.code').on('click', function(e){
    e.preventDefault();

        // first pause the audio (in case it is still playing)
        toast.pause();
        // reset the audio
        toast.currentTime = 0;
        // play audio
        toast.play();
    $("#product").html($(this).data('product'));
    $("#code").html($(this).data('code'));
    $('#toast').toast({autohide: false}).toast('show');

    });
});