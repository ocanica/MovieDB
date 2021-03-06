﻿
function drawRatings () {
    $('.anchorFavouriteMovie').each(function () {
        var fav = $(this);
        var idArray = $(this).data('id');
        var bool = idArray[1];

        if (bool == 'True') {
            $(fav).html('<i class="fas fa-star"></i>');
        } else if (bool == 'False') {
            $(fav).html('<i class="far fa-star"></i>');
        }
    });
}

drawRatings();

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
                $('#searchWarning span').html('Unable to find <strong>' + query + '</strong>, please try again.');
            }
        });

        event.preventDefault();
    });
});

$(function () {
    $("#viewAllContent").delegate(".anchorDetails", "click", function () {
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
    $("#viewAllContent").delegate(".anchorAddMovie", "click", function () {
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
    $("#catalogue").delegate(".anchorRemoveMovie", "click", function () {
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
    $("#catalogue").delegate(".anchorFavouriteMovie", "click", function () {
        var star = $(this);
        var url = $(this).data('url');
        var idArray = $(this).data('id');
        var id = idArray[0];
        var bool = idArray[1];

        $.ajax({
            type: 'GET',
            url: '/Movie/ToggleFavourite',
            data: { 'imdbID': id, 'favouriteBool': bool},
            success: function (data) {
                if (data == 'True') {
                    $('#'+id).html('<i class="fas fa-star"></i>');
                } else if (data == 'False') {
                    $('#'+id).html('<i class="far fa-star"></i>');
                }
            }
        });
    });
});

