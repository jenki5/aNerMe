const addCategoryBtn = document.getElementById('add-category-btn');
const addCategoryInput = document.getElementById('add-category-input');
const categoriesDiv = document.getElementById('typed-categories');

addCategoryBtn.addEventListener('click', (e) => {
    if(addCategoryInput.value.trim() != ""){
        const newCategory = createCategoryDiv(addCategoryInput.value);
        categoriesDiv.appendChild(newCategory);
        addCategoryValues();
    }
    addCategoryInput.value = "";
    addCategoryInput.focus();
})

function createCategoryDiv(cat){
    const newCategory = document.createElement('div');
    newCategory.className = "category";
    newCategory.innerHTML = `
        ${cat} <i class="fa fa-times" id="delete-category-${categoriesDiv.children.length + 1}" onclick="deleteCategory(this.id)" aria-hidden="true"></i>
    `    
    return newCategory;
}

function deleteCategory(id){
    document.getElementById(id).parentElement.remove();
    addCategoryValues();
}

addCategoryInput.addEventListener('keypress', function(e){
    if(e.keyCode == 13 || e.which == 13){
        e.preventDefault();
        addCategoryBtn.dispatchEvent(new Event('click'));
        return false;
    }
})

function addCategoryValues(){
    const categories = document.querySelectorAll('.category');
    var imageSelectArr = [];
    categories.forEach(cat => {
        imageSelectArr.push(cat.innerText);
    })
    document.getElementById('categories-string').value = imageSelectArr;
}