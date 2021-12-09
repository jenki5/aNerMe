document.querySelectorAll('.drop-zone-input').forEach(inputElement => {
    const dropZoneElement = inputElement.closest('.drop-zone');

    dropZoneElement.addEventListener('click', e => {
        inputElement.click();
    });

    inputElement.addEventListener('change', e => {
        if(inputElement.files.length){
            SingleImageUpload(inputElement.files[0]);
            updateThumbnail(dropZoneElement, inputElement.files[0]);
        }
    });

    dropZoneElement.addEventListener('dragover', e => {
        e.preventDefault();
        dropZoneElement.classList.add('drop-zone-over');
    });

    ['dragleave', 'dragend'].forEach(type => {
        dropZoneElement.addEventListener(type, e => {
            dropZoneElement.classList.remove('drop-zone-over');
        });
    });

    dropZoneElement.addEventListener('drop', e => {
        e.preventDefault();

        if(e.dataTransfer.files.length){
            inputElement.files = e.dataTransfer.files;
            SingleImageUpload(inputElement.files[0]);
            updateThumbnail(dropZoneElement, e.dataTransfer.files[0]);
        }

        dropZoneElement.classList.remove("drop-zone-over");
    });
});

function updateThumbnail(dropZoneElement, file){
    let thumbnailElement = dropZoneElement.querySelector('.drop-zone-thumb');

    if(dropZoneElement.querySelector('.drop-zone-prompt')){
        dropZoneElement.querySelector('.drop-zone-prompt').remove();
    }

    if(!thumbnailElement){
        thumbnailElement = document.createElement('div');
        thumbnailElement.classList.add('drop-zone-thumb');
        dropZoneElement.appendChild(thumbnailElement);
    }

    thumbnailElement.dataset.label = file.name;
    if(file.type.startsWith('image/')){
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
            thumbnailElement.style.backgroundImage = `url('${reader.result}')`;
        }
    }
    else{
        thumbnailElement.style.backgroundImage = null;
    }
}

function SingleImageUpload(file) {
    var data = new FormData;
    data.append("ImageFile", file);
    data.append("ImageName", file.name);
    $.ajax({
        type: "Post",
        url: "/Business/SingleImageUpload",
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            companyImages.push(response.message);
            loadModalImages(response.message);
            document.getElementById("my-images-btn").dispatchEvent(new Event('click'));
        },
        error: function () {
            alert("Your image did not load successfully. Please check your internet connection and try again.");
        }
    });
}