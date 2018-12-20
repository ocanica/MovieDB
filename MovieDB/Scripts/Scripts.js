

$(function () {
    $('.anchorFavouriteMovie').each(function () {
        var idArray = $(this).data('id');
        var id = idArray[0];
        var bool = idArray[1];

        if (bool == 'True') {
            $('#star').html('<i class="fas fa-star"></i>');
        } else if (bool == 'False') {
            $('#star').html('<i class="far fa-star"></i>');
        }
        
    });
});

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
        var url = $(this).data('url');
        $.ajax({
            type: "GET",
            url: '/Movie/AddMovie',
            contentType: "application/json",
            data: { 'imdbID': id },
            success: function () {
                $('#catalogue').load(url);
                $('#movieModal').modal('toggle');
            },
            error: function (xhr) {
                alert("Failed to add movie to library: " + xhr.status);
            }
        });
    });
});

$(function () {
    $("body").delegate(".anchorRemoveMovie", "click", function () {
        var id = $(this).data('id');
        var url = $(this).data('url');
        $.ajax({
            type: 'GET',
            url: '/Movie/RemoveMovie',
            data: { 'imdbID': id },
            success: function () {
                $('#catalogue').load(url);
            }
        });
    });
});

$(function () {
    $("body").delegate(".anchorFavouriteMovie", "click", function () {
        var idArray = $(this).data('id');
        var url = $(this).data('url');

        var id = idArray[0];
        var bool = idArray[1];

        $.ajax({
            type: 'GET',
            url: '/Movie/ToggleFavourite',
            data: { 'imdbID': id, 'favouriteBool': bool},
            success: function () {
                $('#catalogue').load(url);
            }
        });
    });
});