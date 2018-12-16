$(function () {
    $("#searchBox").submit(function (event) {
        var query = $('#searchQuery').val();
        $.ajax({
            type: "POST",
            url: '/Movie/ViewAllMovies',
            data: { 'searchQuery': query },
            success: function (data) {
                $('#viewAllContent').html(data);
            },
            error: function () {
                $('#viewAllContent').html('unable to render page!');
            }
        });

        event.preventDefault();
    });
});


$(function () {
    $("body").delegate(".anchorDetails", "click", function () {
        var id = $(this).data('id');
        $.ajax({
            type: "GET",
            url: '/Movie/ViewDetails',
            contentType: "application/json",
            data: { 'imdbID': id },
            datatype: "json",
            success: function (data) {
                $('#movieModalContent').html(data);
                $('#movieModal').modal('show');
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
});





