const uploadImagesBtn = document.getElementById('upload-images');
const myImagesBtn = document.getElementById('my-images-btn');
const dropZone = document.getElementById('drop-zone');
const myImagesDiv = document.getElementById('my-images-div');
const openModalBtn = document.getElementById('open-modal');
const overlay = document.querySelector('.overlay');
const modal = document.querySelector('.image-popup-single-select');
var imageSelectArr = [];

myImagesBtn.addEventListener('click', (e) => {
    e.preventDefault();
    uploadImagesBtn.classList.remove('selected');
    myImagesBtn.classList.add('selected');
    dropZone.style.display = 'none';
    myImagesDiv.style.display = 'inherit';
})

uploadImagesBtn.addEventListener('click', (e) => {
    e.preventDefault();
    myImagesBtn.classList.remove('selected');
    uploadImagesBtn.classList.add('selected');
    dropZone.style.display = 'flex';
    myImagesDiv.style.display = 'none';
})

overlay.addEventListener('click', () => {
    overlay.style.display = 'none';
    modal.style.display = 'none';
})

openModalBtn.addEventListener('click', () => {
    overlay.style.display = 'block';
    modal.style.display = 'block';
})

function removeImageID(arr, id){
    for(var i = 0; i < arr.length; i++){
        if(arr[i] == id){
            arr.splice(i, 1);
            break;
        }
    }
}

function resetCounters(){
    for(var i = 0; i < imageSelectArr.length; i++){
        const counter = document.getElementById(`counter-${imageSelectArr[i]}`);
        counter.innerText = `${i+1}`;
    }
}

function imageSelect(id){
    const image = document.getElementById(id);
    image.parentElement.classList.toggle('selected');
    var imageID = id.substring(id.lastIndexOf('-') + 1);
    if(image.parentElement.children.length > 1){
        image.parentElement.lastChild.remove();
        removeImageID(imageSelectArr, imageID);
        resetCounters();
    }
    else{
        imageSelectArr.push(imageID);
        const counter = document.createElement('div');
        counter.className = "counter";
        counter.id = `counter-${imageID}`;
        counter.innerText = `${imageSelectArr.length}`;
        image.parentElement.appendChild(counter);
    }
}

function loadModalImages(image){
    const modalImages = document.getElementById("modal-images-grid");
    const imageBox = document.createElement('div');
    imageBox.className = "my-image-box";
    imageBox.id = `image-box-${image.imageID}`;
    imageBox.innerHTML = `<img src="${image.imagePath}" id="image-${image.imageID}" onclick="imageSelect(this.id)" alt="">`;
    modalImages.appendChild(imageBox);
}

for(var i = 0; i < companyImages.length; i++){
    loadModalImages(companyImages[i]);
}