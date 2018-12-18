$(function () {
    $("#searchBox").submit(function (event) {
        var query = $('#searchQuery').val();
        $.ajax({
            type: "POST",
            url: '/Movie/ViewAllMovies',
            data: { 'searchQuery': query },
            success: function (data) {
                $('#viewAllContent').show();
                $('#viewAllContent').html(data);
                $('#searchWarning').hide();
            },
            error: function () {
                $('#viewAllContent').hide();
                $('#searchWarning span').html('Unable to find <strong>' + query + '</strong>, please try again.')
                $('#searchWarning').show();
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
                alert("Failed to load requested movie.");
            }
        });
    });
});

$(function () {
    $("body").delegate(".anchorAddMovie", "click", function () {
        var id = $(this).data('id');
        $.ajax({
            type: "GET",
            url: '/Movie/AddMovie',
            contentType: "application/json",
            data: { 'imdbID': id },
            success: function () {
                $('#movieModal').modal('toggle');
            },
            error: function (xhr) {
                alert("Failed to add movie to library: " + xhr.status);
            }
        });
    });
});