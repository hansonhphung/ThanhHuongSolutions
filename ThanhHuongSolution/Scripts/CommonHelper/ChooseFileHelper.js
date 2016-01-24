var FileControl = function ()
{
}

FileControl.prototype.BindingImage = function(input, imageControl)
{
    if (input.files && input.files[0])
        var reader = new FileReader();

    reader.onload = function (e) {
        $(imageControl).attr('src', e.target.result);
    }

    reader.readAsDataURL(input.files[0]);
}