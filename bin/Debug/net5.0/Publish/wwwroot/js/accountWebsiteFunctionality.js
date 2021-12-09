const bannerSelector = document.getElementById('banner-selector-page-div');
const logoAdder = document.getElementById('logo-adder-page-div');
const bannerColorSelector = document.getElementById('banner-color-selector-page-div');
const menuBuilder = document.getElementById('menu-builder-page-div');
const menuContentBuilder = document.getElementById('menu-content-page-div');
const nextBtn = document.getElementById('next');
const prevBtn = document.getElementById('prev');
const menuInputsDiv = document.getElementById('menu-builder-inputs');
const menuDirectorsDiv = document.getElementById('menu-builder-directors');
var bannerUl = document.getElementById('company-ul');
const addMenuItemBtn = document.querySelector('.add-menu-item-btn');


var page = 0;


//----------------Navigatoin and Progress Bar----------------//
showPage();

nextBtn.addEventListener('click', () => {
    progressErrors = 0;
    if(page == 3){
        countErrors();
    }
    const errorMessage = document.getElementById('error-message');
    if(progressErrors > 0){
        errorMessage.innerText = "Please correct the errors below before proceeding."
        errorMessage.style.color = "red";
    }
    else{
        errorMessage.innerText = "";
        page++;
        showPage();
    }
})

prevBtn.addEventListener('click', () => {
    page--;
    showPage();
})

function showPage(){
    logoAdder.style.display = 'none';
    bannerColorSelector.style.display = 'none';
    menuBuilder.style.display = 'none';
    menuContentBuilder.style.display = 'none';
    bannerSelector.style.display = 'none';

    switch(page){
        case 0:
            bannerSelector.style.display = 'block';            
            break;
        case 1:
            logoAdder.style.display = 'block';
            break;
        case 2:
            bannerColorSelector.style.display = 'block';
            textItems = document.querySelectorAll('.set-text-color');
            break;
        case 3:
            menuBuilder.style.display = 'block';
            bannerUl = document.getElementById('company-ul');
            break;
        case 4:
            menuContentBuilder.style.display = 'block';
            break;
    }
}

//-------------------------Menu Builder Functionality-------------------------------------------//

const menuBuilderDiv = document.getElementById('dropdown-filler-space');
const progressBar = document.querySelector('.progress-bar-div');
const menuBuilderHeader = document.getElementById('menu-builder-header');
var menuItemsLS = JSON.parse(localStorage.getItem("menuText"));
const selectBannerBtn = document.querySelectorAll('.select-banner-btn');
menuBuilderDiv.addEventListener('click', () => removeShow());
progressBar.addEventListener('click', () => removeShow());
menuBuilderHeader.addEventListener('click', () => removeShow());

function loadStoredSettings(){
    document.querySelectorAll('.select-banner-btn')[dbBanner.bannerPartialID - 1].dispatchEvent(new Event('click'));
    textItems = document.querySelectorAll('.set-text-color');
    banner.style.backgroundColor = dbBanner.bannerColor;
    let color = dbBanner.bannerColor;
    let shade = lightOrDark(color);
    if(shade.trim() == 'dark'){
        setTextColor(textItems, '#fff');
    }
    else{
        setTextColor(textItems, '#000');
    }
    document.getElementById(`image-${dbBanner.imageID}`).dispatchEvent(new Event('click'));
    document.getElementById('upload-image-btn').dispatchEvent(new Event('click'));
}

selectBannerBtn.forEach(btn => {
    btn.addEventListener('click', () => {
        const selectedBanner = document.getElementById('selected-company-banner');
        const menuBuilderInputs = document.getElementById('menu-builder-inputs');
        menuBuilderInputs.innerHTML = "";
        selectedBanner.innerHTML = "";
        const cloneBanner = btn.previousElementSibling.cloneNode(true);
        if(btn.innerText == "Banner 1"){
            cloneBanner.id = "company-banner";
            cloneBanner.children[1].id = "company-ul";
            cloneBanner.children[1].innerHTML = "";
        }
        else if(btn.innerText == "Banner 2"){
            cloneBanner.children[1].id = "company-banner";
            cloneBanner.children[1].children[0].children[0].id = "company-ul";
            cloneBanner.children[1].children[0].children[0].innerHTML = "";
        }
        
        selectedBanner.appendChild(cloneBanner);
        banner = document.getElementById('company-banner');
        setNewBanner();
    })
})

if(dbBanner)
{
    if(dbBanner.bannerPartialID)
    {
        selectBannerBtn[dbBanner.bannerPartialID-1].dispatchEvent(new Event('click'));
    }
}

function countErrors(){
    const inputs = document.querySelectorAll('.menu-builder-input');
    for(var i = 0; i < inputs.length; i++){
        if(inputs[i].id.substring(0, 18) == "menu-builder-input"){
            if(inputs[i].value == ""){
                inputs[i].style.borderColor = "red";
                progressErrors++;
            }
            if(inputs[i].nextElementSibling.children[0].children[0].value == 0){
                inputs[i].nextElementSibling.children[0].children[0].style.borderColor = "red";
                progressErrors++;
            }
            else if(inputs[i].nextElementSibling.children[0].children[0].value == 1){
                if(inputs[i].nextElementSibling.children[1].children[0].value == 0){
                    inputs[i].nextElementSibling.children[1].children[0].style.borderColor = "red";
                    inputs[i].nextElementSibling.children[0].children[0].style.borderColor = "red";
                    progressErrors++;
                }
            }
        }
        else if(inputs[i].id.substring(0, 27) == "dropdown-menu-builder-input"){
            if(inputs[i].parentElement.parentElement.parentElement.parentElement.children[0].children[0].value == 3){
                if(inputs[i].value == ""){
                    inputs[i].style.borderColor = "red";
                    inputs[i].parentElement.parentElement.parentElement.parentElement.children[0].children[0].style.borderColor = "red";
                    progressErrors++;
                }
                if(inputs[i].nextElementSibling.value == 0){
                    inputs[i].nextElementSibling.style.borderColor = "red";
                    inputs[i].parentElement.parentElement.parentElement.parentElement.children[0].children[0].style.borderColor = "red";
                    progressErrors++;
                }
            }
        }
    }
}

//Add menu items inputs
function setNewBanner(){
    console.log(dbBanner);
    if(dbBanner){
        setDBMenu();
        rebuildBanner();
    }
    else if(menuItemsLS){
        setLSMenu();
        rebuildBanner();
    }
    else{
        document.querySelector('.company-ul').innerHTML = "";
        for(var i = 1; i < 5; i++){
            const newMenuController = createMenuController(i, "", 0, 1);
            newMenuController.children[1].children[1].children[2].classList.add("middle");
            menuInputsDiv.appendChild(newMenuController);
            const dropdownOptionDisplay = document.getElementById(`dropdown-option-container-${i}`);
            for(var x = 1; x < 5; x++){
                dropdownOptionDisplay.innerHTML += createDropdownOptionControllerDiv(i, x, "", 0);
            }
            const menuController = document.getElementById(`menu-controller-${i}`);
            menuController.addEventListener('dragstart', () => menuOnDragStart(menuController));
            menuController.addEventListener('dragend', () => menuOnDragEnd(menuController));    
        
            createLI(i);
        }
    }
}

addMenuItemBtn.addEventListener('click', () => {
    addMenuItem();
});

menuInputsDiv.addEventListener('dragover', (e) => {
    e.preventDefault();
    const afterElement = getDragAfterElementHorizontal(menuInputsDiv, e.clientX);
    const draggable = document.querySelector('.menu-dragging');
    if(afterElement == null){
        menuInputsDiv.appendChild(draggable);
    }
    else {
        menuInputsDiv.insertBefore(draggable, afterElement);
    }
})

function createMenuController(num, menuText, menuAction, ltpAction){
    const newMenuController = document.createElement('div');
    newMenuController.className = "menu-controller";
    newMenuController.id = `menu-controller-${num}`;
    newMenuController.draggable = true;
    newMenuController.innerHTML = '<i class="fa fa-bars menu-movers" aria-hidden="true"></i>';
    const inputBox = document.createElement('div');
    inputBox.className = "menu-controller-input-box";
    inputBox.appendChild(createMenuInput(num, menuText));
    inputBox.appendChild(createNewDirector(num, menuAction, ltpAction));
    //inputBox.appendChild(existingMenuFeatures(num-1));
    newMenuController.appendChild(inputBox);
    return newMenuController;
}

function existingMenuFeatures(num){
    console.log(dbBanner.menuItems[num]);
    const featuresDiv = document.createElement('div');
    featuresDiv.innerHTML = `
        <input type="hidden" class="contact-us-page-id" value="${dbBanner.menuItems[num].contactUsPageID}">
        <input type="hidden" class="information-page-id" value="${dbBanner.menuItems[num].informationPageID}">
    `
    return featuresDiv;
}

//Create input for menu items
function createMenuInput(num, menuText){
    //create input element
    const newInput = document.createElement('input');
    newInput.type = 'text';
    newInput.placeholder = `Example ${num}`;
    newInput.id = `menu-builder-input-${num}`;
    newInput.className = 'menu-builder-input main-menu';
    newInput.value = menuText;
    
    //add event listener
    newInput.addEventListener('keyup', () => {
        editMenu(newInput.id);
    })
    newInput.addEventListener('change', () => {
        editMenu(newInput.id);
        updateLS();
    })
    return newInput;
}

function editMenu(id){
    const menuInput = document.getElementById(id);
    id = id.substring(id.lastIndexOf('-') + 1);
    const menuItem = document.getElementById(`menu-item-${id}`);
    
    if(menuInput.value.trim() == ""){
        menuItem.innerText = menuInput.placeholder;
        menuInput.style.borderColor = "red";
    }
    else{
        menuItem.innerText = menuInput.value;
        menuInput.style.bordercolor = "black";
    }
}

function createNewDirector(num, menuAction, ltpAction){
    //Create director-div
    const newDirectorDiv = document.createElement('div');
    newDirectorDiv.className = "director-div";
    newDirectorDiv.id = `director-div-${num}`;

    //create select-div
    const newSelectDiv = createDirectorSelectDiv(num, menuAction);
    newDirectorDiv.appendChild(newSelectDiv);

    const newLTPDiv = createLTPDiv(num, ltpAction);
    newDirectorDiv.appendChild(newLTPDiv);

    const dropdownOptionDisplay = createDropdownOptionDisplay(num);
    newDirectorDiv.appendChild(dropdownOptionDisplay);

    return newDirectorDiv;
}

function createDropdownOptionDisplay(num){
    const dropdownOptionDisplay = document.createElement('div');
    dropdownOptionDisplay.className = 'dropdown-option-display';
    dropdownOptionDisplay.id = `dropdown-option-display-${num}`;

    dropdownOptionDisplay.innerHTML += `<div id="dropdown-option-container-${num}" ondragover="dropdownOnDragOver(event, this.id)"></div>`
    dropdownOptionDisplay.innerHTML += `<button class="btn btn-primary" id="add-dropdown-menu-item-btn-${num}" onclick="addDropdownMenuItem(event, this.id)">Add Dropdown Menu Item</button>`;

    return dropdownOptionDisplay;
}

function createDropdownOptionControllerDiv(mID, dID, inputText, selectValue){
    var dropdownOptionControllerDiv = `
    <div id="dropdown-option-controller-div-${mID}-${dID}" class="dropdown-option-controller-div" draggable="true" ondragstart="dropdownOnDragStart(this.id)" ondragend="dropdownOnDragEnd(this.id)">
        <i class="fa fa-bars" aria-hidden="true"></i>
        <input type="text" placeholder="Example ${dID}" value="${inputText}" onkeyup="editDropdownItem(this.id)" id="dropdown-menu-builder-input-${mID}-${dID}" class="menu-builder-input dropdown-menu">
        <select id="page-director-${mID}-${dID}" class="menu-builder-director" onchange="updateLS()">
          <option value="null" ${selectValue == 0 ? "selected" : ""}>Choose...</option>
    `;

    for(var i = 0; i < ltpActions.length; i++)
    {
        dropdownOptionControllerDiv += `<option value="${ltpActions[i].ltpActionID}" ${selectValue == ltpActions[i].ltpActionID ? "selected" : ""}>${ltpActions[i].ltpActionName}</option>`;
    }
    dropdownOptionControllerDiv += `
        </select>
        <i class="fa fa-trash" id="trash-${mID}-${dID}" onclick="deleteDropdownMenuItem(this.id)"></i>
    </div>
    `;
    return dropdownOptionControllerDiv;
}

function editDropdownItem(id){
    var idTag = id.substring(28);
    const input = document.getElementById(id);
    const dropdownItem = document.getElementById(`dropdown-item-${idTag}`);
    if(input.value.trim() == ""){
        dropdownItem.innerText = input.placeholder; 
        input.style.borderColor = "red";
    }
    else{
        dropdownItem.innerText = input.value;
        input.style.borderColor = "black";
    }
    updateLS();
}

function createLTPDiv(num, ltpAction){
    const newLinkToPageOptionDisplayDiv = document.createElement('div');
    newLinkToPageOptionDisplayDiv.className = 'link-to-page-option-display';
    newLinkToPageOptionDisplayDiv.id = `link-to-page-option-display-${num}`;

    var dropdownOptionControllerDiv = `
    <select id="page-director-${num}" class="menu-builder-director" onchange="updateLS()">
        <option value="null" ${ltpAction == 0 ? "selected" : ""}>Choose...</option>
    `;

    for(var i = 0; i < ltpActions.length; i++)
    {
        dropdownOptionControllerDiv += `<option value="${ltpActions[i].ltpActionID}" ${ltpAction == ltpActions[i].ltpActionID ? "selected" : ""}>${ltpActions[i].ltpActionName}</option>`;
    }
    dropdownOptionControllerDiv += `
    </select>
    `;

    newLinkToPageOptionDisplayDiv.innerHTML = dropdownOptionControllerDiv;

    return newLinkToPageOptionDisplayDiv;
}

function addDropdownMenuItem(e, id){
    e.preventDefault();
    id = id.substring(id.lastIndexOf('-') + 1);
    dropdownOptionContainer = document.getElementById(`dropdown-option-container-${id}`);
    dropdownOptionContainer.innerHTML += createDropdownOptionControllerDiv(id, dropdownOptionContainer.children.length + 1, "", 0);

    setDropdownFillerSpaceHeight(dropdownOptionContainer.children.length);

    const dropdown = document.getElementById(`dropdown-content-div-${id}`);
    var num = dropdown.children.length + 1;
    dropdown.innerHTML += `<a class="" id="dropdown-item-${id}-${num}" >Example ${num}</a>`;
}

function dropdownOnDragStart(id){
    const draggable = document.getElementById(id);
    draggable.classList.add('dropdown-dragging');    
}

function dropdownOnDragEnd(id){
    const draggable = document.getElementById(id);
    draggable.classList.remove('dropdown-dragging');
    resetIDs();
    updateLS();
}

function menuOnDragStart(menuItem){
    menuItem.classList.add('menu-dragging');
}

function menuOnDragEnd(menuItem){
    menuItem.classList.remove('menu-dragging');
    rebuildBanner();
    updateLS();
}

function dropdownOnDragOver(e, id){
    e.preventDefault();
    const container = document.getElementById(id);
    const afterElement = getDragAfterElement(container, e.clientY);
    const draggable = document.querySelector('.dropdown-dragging');
    if(afterElement == null){
        container.appendChild(draggable);
    }
    else {
        container.insertBefore(draggable, afterElement);
    }
}

function getDragAfterElementHorizontal(container, x){
    const draggableElements = [...container.querySelectorAll('.menu-controller:not(.menu-dragging)')];
    return draggableElements.reduce((closest, child) => {
        const box = child.getBoundingClientRect();
        const offset = x - box.left - box.width / 2;
        if(offset < 0 && offset > closest.offset){
            return {offset: offset, element: child}
        }
        else {
            return closest;
        }
    }, {offset: Number.NEGATIVE_INFINITY} ).element;

}

function getDragAfterElement(container, y){
    const draggableElements = [...container.querySelectorAll('.dropdown-option-controller-div:not(.dropdown-dragging)')];
    return draggableElements.reduce((closest, child) => {
        const box = child.getBoundingClientRect();
        const offset = y - box.top - box.height / 2;
        if(offset < 0 && offset > closest.offset){
            return {offset: offset, element: child}
        }
        else {
            return closest;
        }
    }, {offset: Number.NEGATIVE_INFINITY} ).element;
}

function setDropdownFillerSpaceHeight(num){
    const DropdownFillerSpace = document.getElementById('dropdown-filler-space');
    DropdownFillerSpace.style.height = `${num * 40 + 80}px`;
}

function createLI(num){
    const newLI = document.createElement('li');
    newLI.innerHTML = `
    <div class="menu-content-div" id="menu-content-div-${num}">
        <a class="set-text-color" id="menu-item-${num}" >Example ${num}</a>
    </div>
    `;
    document.getElementById(`company-ul`).appendChild(newLI);
}

function createDropdownManu(menuID, dropdownEditDiv){
    const newDropdown = document.createElement('div');
    newDropdown.className = "dropdown-content-div";
    newDropdown.id = `dropdown-content-div-${menuID}`;

    for(var i = 0; i < dropdownEditDiv.length; i++){
        let word;
        if(dropdownEditDiv[i].children[1].value == ""){
            word = dropdownEditDiv[i].children[1].placeholder;
        }
        else{
            word = dropdownEditDiv[i].children[1].value;
        }
        newDropdown.innerHTML += `<a class="" id="dropdown-item-${menuID}-${i+1}" >${word}</a>`;
    }

    document.getElementById(`menu-content-div-${menuID}`).appendChild(newDropdown);
}

function deleteDropdownMenu(num){
    document.getElementById(`dropdown-content-div-${num}`).remove();
}

function createDirectorSelectDiv(num, menuAction){
    const newSelectDiv = document.createElement('div');
    newSelectDiv.className = 'select-div';
    newSelectDiv.id = `select-div-${num}`;
    console.log(menuAction);
    var htmlString = `
        <select id="menu-builder-director-${num}" onclick="displayOption(this.id, this.value)" class="menu-builder-director menu-select" onchange="updateLS()">
            <option value="null" ${menuAction == 0 ? "selected" : ""}>Choose...</option>
        `;

    for(var i = 0; i < menuActions.length; i++)
    {
        htmlString += `<option value="${menuActions[i].menuActionID}" ${menuAction == menuActions[i].menuActionID ? "selected" : ""}>${menuActions[i].menuActionName}</option>`;
    }

    htmlString += `
    </select>
    <i class="fa fa-trash" id="trash-${num}" onclick="deleteMenuItem(this.id)"></i>
    <div class="line-eraser"></div>
    `;

    newSelectDiv.innerHTML += htmlString;
    return newSelectDiv;
}

function deleteDropdownMenuItem(id){
    var idTag = id.substring(6);
    const dropdownMenuDiv = document.getElementById(`dropdown-option-controller-div-${idTag}`);
    dropdownMenuDiv.remove();
    var menuID = idTag.substring(0, idTag.indexOf('-'));
    const dropdownOptionContainer = document.getElementById(`dropdown-option-container-${menuID}`);
    document.getElementById(`dropdown-item-${idTag}`).remove();
    setDropdownFillerSpaceHeight(dropdownOptionContainer.children.length);
    resetIDs();
    updateLS();
}

function deleteMenuItem(id){
    var num = menuInputsDiv.children.length;
    if(num == 4){
        alert("You need at least 4 menu items.")
    }
    else{
        id = id.substring(id.lastIndexOf('-') + 1);
        const menuController = document.getElementById(`menu-controller-${id}`);
        menuController.remove();
        deleteLI(id);
        resetIDs();
        updateLS();
    }
}

function deleteLI(num){
    document.getElementById(`menu-content-div-${num}`).parentElement.remove();
}

function addMenuItem(){
    var num = menuInputsDiv.children.length + 1;
    if(num < 7){
        const newMenuController = createMenuController(num, "", 0, 1);
        newMenuController.children[1].children[1].children[2].classList.add("middle");
        menuInputsDiv.appendChild(newMenuController);
        newMenuController.addEventListener('dragstart', () => menuOnDragStart(newMenuController));
        newMenuController.addEventListener('dragend', () => menuOnDragEnd(newMenuController));
        const dropdownOptionDisplay = document.getElementById(`dropdown-option-container-${num}`);
        for(var x = 1; x < 5; x++){
            dropdownOptionDisplay.innerHTML += createDropdownOptionControllerDiv(num, x, "", 0);
        }
    
        createLI(num);
        updateLS();
    }
    else{
        alert("You cannot add any more menu options");
    }
}

function resetIDs(){
    const menuControllers = document.querySelectorAll('.menu-controller');
    const menuBuilderInput = document.querySelectorAll('.main-menu');
    const directorDivs = document.querySelectorAll('.director-div');
    const companyUL = document.getElementById('company-ul');
    for(var i = 1; i < menuControllers.length + 1; i++){
        // update input area ids
        menuControllers[i-1].id = `menu-controller-${i}`;
        menuBuilderInput[i-1].id = `menu-builder-input-${i}`;
        menuBuilderInput[i-1].placeholder = `Example ${i}`;
        directorDivs[i-1].id = `director-div-${i}`;
        directorDivs[i-1].children[0].id = `select-div-${i}`;
        directorDivs[i-1].children[0].children[0].id = `menu-builder-director-${i}`;
        directorDivs[i-1].children[0].children[1].id = `trash-${i}`;
        directorDivs[i-1].children[1].id = `link-to-page-option-display-${i}`;
        directorDivs[i-1].children[1].children[0].id = `page-director-${i}`;
        directorDivs[i-1].children[2].id = `dropdown-option-display-${i}`;
        directorDivs[i-1].children[2].children[0].id = `dropdown-option-container-${i}`;
        for(var x = 1; x < directorDivs[i-1].children[2].children[0].children.length + 1; x++){
            directorDivs[i-1].children[2].children[0].children[x-1].id = `dropdown-option-controller-div-${i}-${x}`;
            directorDivs[i-1].children[2].children[0].children[x-1].children[1].id = `dropdown-menu-builder-input-${i}-${x}`;
            directorDivs[i-1].children[2].children[0].children[x-1].children[1].placeholder = `Example ${x}`;
            directorDivs[i-1].children[2].children[0].children[x-1].children[2].id = `page-director-${i}-${x}`;
            directorDivs[i-1].children[2].children[0].children[x-1].children[3].id = `trash-${i}-${x}`;
        };

        // Update banner ids
        companyUL.children[i-1].children[0].id = `menu-content-div-${i}`;
        companyUL.children[i-1].children[0].children[0].id = `menu-item-${i}`;
        const input = document.getElementById(`menu-builder-input-${i}`);
        if(input.value.trim() == ""){
            companyUL.children[i-1].children[0].children[0].innerText = input.placeholder;   
        }
        else{
            companyUL.children[i-1].children[0].children[0].innerText = input.value;
        }

        //update dorpdown boxes        
        if(companyUL.children[i-1].children[0].children.length == 2){
            for(var x = 1; x < companyUL.children[i-1].children[0].children[1].children.length + 1; x++){
                companyUL.children[i-1].children[0].children[1].children[x-1].id = `dropdown-item-${i}-${x}`;
                const ddInput = document.getElementById(`dropdown-menu-builder-input-${i}-${x}`);
                if(ddInput.value.trim() == ""){
                    companyUL.children[i-1].children[0].children[1].children[x-1].innerText = ddInput.placeholder;   
                }
                else{
                    companyUL.children[i-1].children[0].children[1].children[x-1].innerText = ddInput.value;
                }
            }
        }
        directorDivs[i-1].children[2].children[1].id = `add-dropdown-menu-item-btn-${i}`;
    }
}

function rebuildBanner(){
    const dropdownDivs = document.querySelectorAll('.dropdown-content-div');
    const menuSelects = document.querySelectorAll(".menu-select");
    dropdownDivs.forEach(dropdown => dropdown.remove());

    resetIDs();
    menuSelects.forEach(menu => {
        menu.dispatchEvent(new Event('click'));
    });
    removeShow();
    updateLS();
}

function displayOption(id, value){
    id = id.substring(id.lastIndexOf('-') + 1);
    const LTPOptionDisplay = document.getElementById(`link-to-page-option-display-${id}`);
    const dropdownOptionDisplay = document.getElementById(`dropdown-option-display-${id}`);
    const selectDiv = document.getElementById(`select-div-${id}`);
    const dropdown = document.getElementById(`dropdown-content-div-${id}`);
    removeShow();
    
    if(value == 1){
        dropdownOptionDisplay.classList.remove('show');
        selectDiv.classList.remove('show');
        LTPOptionDisplay.classList.add('show');
        setDropdownFillerSpaceHeight(1);
        if(dropdown != null){
            deleteDropdownMenu(id);
        }
    }
    else if(value == 2){
        LTPOptionDisplay.classList.remove('show');
        dropdownOptionDisplay.classList.add('show');
        selectDiv.classList.add('show');
        setDropdownFillerSpaceHeight(dropdownOptionDisplay.children[0].children.length);
        if(dropdown == null){
            const ddContainer = document.getElementById(`dropdown-option-container-${id}`);
            createDropdownManu(id, ddContainer.children);
        }
        document.getElementById(`dropdown-content-div-${id}`).classList.add('show');
    }
    else{
        LTPOptionDisplay.classList.remove('show');
        selectDiv.classList.remove('show');
        dropdownOptionDisplay.classList.remove('show');
        setDropdownFillerSpaceHeight(0);
        if(dropdown != null){
            deleteDropdownMenu(id);
        }
    }
}

function removeShow(){
    const showDivs = document.querySelectorAll('.show');
    setDropdownFillerSpaceHeight(0);
    showDivs.forEach(element => {
        element.classList.remove('show');
    });
}

function updateLS(){
    const menuItemElements = document.querySelectorAll('.menu-controller');
    const menuItems = [];
    menuItemElements.forEach(element => {
        let menuTextInput;
        menuTextInput = element.children[1].children[0].value;

        let dropdownContainer = element.children[1].children[1].children[2].children[0];
        const dropdownMenuItems = [];
        for(var i = 0; i < dropdownContainer.children.length; i++){
            let dropdownInput;

            if(!dropdownContainer.children[i].children[1].value){
                dropdownInput = "";
            }
            else{
                dropdownInput = dropdownContainer.children[i].children[1].value;
            }
            dropdownMenuItems.push({
                ddMenuText: dropdownInput,
                ddLTPAction: dropdownContainer.children[i].children[2].value
            })
        }

        menuItems.push({
            menuText: menuTextInput,
            menuAction: element.children[1].children[1].children[0].children[0].value,
            dropdownOptions: dropdownMenuItems,
            ltpAction: element.children[1].children[1].children[1].children[0].value
        });
    });

    localStorage.setItem("menuText", JSON.stringify(menuItems));
    menuItemsLS = JSON.parse(localStorage.getItem("menuText"));
}

function setLSMenu(){
    for(var i = 0; i < menuItemsLS.length; i++){
        const newMenuController = createMenuController(i+1, menuItemsLS[i].menuText, menuItemsLS[i].menuAction, menuItemsLS[i].ltpAction);
        newMenuController.children[1].children[1].children[2].classList.add("middle");
        menuInputsDiv.appendChild(newMenuController);
        const dropdownOptionDisplay = document.getElementById(`dropdown-option-container-${i+1}`);
        for(var x = 0; x < menuItemsLS[i].dropdownOptions.length; x++){
            dropdownOptionDisplay.innerHTML += createDropdownOptionControllerDiv(i+1, x+1, menuItemsLS[i].dropdownOptions[x].ddMenuText, menuItemsLS[i].dropdownOptions[x].ddLTPAction);
        }
        const menuController = document.getElementById(`menu-controller-${i+1}`);
        menuController.addEventListener('dragstart', () => menuOnDragStart(menuController));
        menuController.addEventListener('dragend', () => menuOnDragEnd(menuController));    
    
        createLI(i+1);
    }
}

function setDBMenu(){
    for(var i = 0; i < dbBanner.menuItems.length; i++){
        const newMenuController = createMenuController(i+1, dbBanner.menuItems[i].title, dbBanner.menuItems[i].menuActionID, dbBanner.menuItems[i].ltpActionID);
        newMenuController.children[1].children[1].children[2].classList.add("middle");
        menuInputsDiv.appendChild(newMenuController);
        const dropdownOptionDisplay = document.getElementById(`dropdown-option-container-${i+1}`);
        for(var x = 0; x < dbBanner.menuItems[i].dropDownItems.length; x++){
            dropdownOptionDisplay.innerHTML += createDropdownOptionControllerDiv(i+1, x+1, dbBanner.menuItems[i].dropDownItems[x].title, dbBanner.menuItems[i].dropDownItems[x].ltpActionID);
        }
        const menuController = document.getElementById(`menu-controller-${i+1}`);
        menuController.addEventListener('dragstart', () => menuOnDragStart(menuController));
        menuController.addEventListener('dragend', () => menuOnDragEnd(menuController));    
    
        createLI(i+1);
    }

}

document.getElementById("submit-banner").addEventListener('click', () => {
    const MenuItems = [];

    for(var i = 0; i < menuItemsLS.length; i++){
        const menuItem = new MenuItem(menuItemsLS[i].menuText, menuItemsLS[i].ltpAction, menuItemsLS[i].menuAction,  (i+1));
        if(menuItemsLS[i].menuAction == 2 && menuItemsLS.dropdownOptions == null){
            for(var x = 0; x < menuItemsLS[i].dropdownOptions.length; x++){
                const dropdownMenuItem = new MenuItem(menuItemsLS[i].dropdownOptions[x].ddMenuText, menuItemsLS[i].dropdownOptions[x].ddLTPAction, 1,  (x+1));
                menuItem.DropDownItems.push(dropdownMenuItem);
            }
        }
        MenuItems.push(menuItem);
    }

    const color = banner.style.backgroundColor;
    let imageID = document.querySelector(".selected-image").id;
    imageID = imageID.substring(imageID.lastIndexOf('-') + 1);
    let bannerPartialID = document.getElementById('selected-company-banner').firstChild.classList[1];
    bannerPartialID = bannerPartialID.substring(bannerPartialID.lastIndexOf('-') + 1);
    
    const newBanner = new Banner (color, imageID, MenuItems, bannerPartialID);

    $.ajax({
        type: "POST",
        url: '/Business/SubmitBanner',
        dataType: "json",
        data: { banner: newBanner, menuItems: MenuItems },
        success: function (Message) {
            document.location.reload(true);
        },
        error: function () {
            alert("Save Unsuccessful: Please check your internet connection and submit again.");
        }
    });
});

class Banner{
    constructor(BannerColor, ImageID, MenuItems, BannerPartialID){
        this.BannerColor = BannerColor;
        this.ImageID = ImageID;
        this.MenuItems = MenuItems;
        this.BannerPartialID = BannerPartialID;
    }
}

class MenuItem{
    constructor(Title, LTPActionID, MenuActionID, OrderNumber){
        this.Title = Title;
        this.LTPActionID = LTPActionID;
        this.MenuActionID = MenuActionID == 0 ? null: MenuActionID;
        this.OrderNumber = OrderNumber;
        this.DropDownItems = [];
    }
}