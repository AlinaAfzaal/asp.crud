window.setTimeout(function () {
    $('.alert').fadeTo(500, 0).slideUp(1000, function () {
        $(this).remove();  
    })
}, 1000)