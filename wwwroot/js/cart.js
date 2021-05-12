$(function () {
    calculateTotal();
    $(".delete").on('click', function(){
        console.log("working");
        var cartItemId = $(this).data('id');
        console.log(cartItemId);
        $.ajax({
            url: "../../api/removefromcart/" + cartItemId,
            type: 'delete',
            success: function (response, textStatus, jqXhr) {
                console.log('Success, removed item' + response);
                window.location.reload();
            },
        })

    })

    function calculateTotal(){
        var total = 0;
            $('tr').each(function(){
                var $cost = $(this).data('cost');
                var $qty = $(this).data('quantity');

                if($cost){
                    total += (parseFloat($cost * $qty));
                    console.log($cost * $qty);
                    console.log($qty);
                }

        })
            
        console.log(total);
        $("#totalCost").append('$' + parseFloat(total).toFixed(2));
    }
    $("#checkOutCart").on('click', function(){
        console.log("working");
        var email = $("#user").data('email');
        $.ajax({
            url: "../../api/checkout/" + email,
            type: 'put',
            success: function (response, textStatus, jqXhr) {
                console.log('Success' + response);
                $(".cartMain").html('<div class="jumbotron text-center"><h2>Successfully Checked out</h2></div>');
                
            },
        })
    })
});