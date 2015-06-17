

function LoadImages()
{
    $.ajax({
        url: "http://localhost:64626/api/artefacts"
    }).then(function (data) {


        data.forEach(function (artefact) {
            $('.Title' + artefact.Id).append(artefact.Title);
            var imgContent = artefact.ImagePages[0].Base64Data;
            var imageString = 'data:image/png;base64,' + imgContent;

            localStorage.setItem(artefact.Id, imageString);
            $('#img' + artefact.Id).attr('src', imageString);
        });
    });
}

function ReadImagesFromMem()
{
    var IDs = [1, 2, 3];
    IDs.forEach(function (id) {

        var imageString = localStorage.getItem(id);
        $('#img' + id).attr('src', imageString);
    });
}

$(document).ready(function () {

    var exists = localStorage.getItem("1");
    if (exists != null)
    {
        ReadImagesFromMem();
    }
    else
    {
        LoadImages();
    }
});

