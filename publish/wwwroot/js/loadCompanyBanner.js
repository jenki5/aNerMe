console.log(dbCompany);
var companyBanner;
const bannerUL = document.querySelector('.company-ul');
const imageBox = document.querySelector('.image-box');
let color = dbCompany.banner.bannerColor;
let shade = lightOrDark(color);

if(dbCompany.banner.bannerPartialID == 1){
    companyBanner = document.querySelector('.banner');
}
else if(dbCompany.banner.bannerPartialID == 2){
    companyBanner = document.querySelector('.company-banner-2-layer-2');
}

imageBox.innerHTML = `
    <img src="${dbCompany.banner.image.imagePath}" alt="Your Logo">
`;

companyBanner.style.backgroundColor = color;

bannerUL.innerHTML = "";

for(let i = 0; i < dbCompany.banner.menuItems.length; i++){
    let liString = `
    <li>
        <div class="menu-content-div">
    `;

    if(dbCompany.banner.menuItems[i].menuActionID == 1)
    {
        liString += `
            <a class="set-text-color" href="/Local/${dbCompany.companyName}/${dbCompany.companyID}/${dbCompany.banner.menuItems[i].menuItemID}">${dbCompany.banner.menuItems[i].title}</a>
        `;
    }
    else
    {
        liString += `
            <a class="set-text-color">${dbCompany.banner.menuItems[i].title}</a>
        `;
    }

    if(dbCompany.banner.menuItems[i].dropDownItems != null && dbCompany.banner.menuItems[i].dropDownItems.length != 0){
        liString += `<div class="dropdown-content-div">`;
        for(let x = 0; x < dbCompany.banner.menuItems[i].dropDownItems.length; x++){
            liString += `
                <a href="/Local/${dbCompany.companyName}/${dbCompany.companyID}/${dbCompany.banner.menuItems[i].dropDownItems[x].menuItemID}">${dbCompany.banner.menuItems[i].dropDownItems[x].title}</a>
            `;
        }
        liString += `</div>`;
    }

    liString += `
            </div>
        </li>
    `;

    bannerUL.innerHTML += liString;
};

textItems = document.querySelectorAll('.set-text-color');

if(shade.trim() == 'dark'){
    setTextColor(textItems, '#fff');
}
else{
    setTextColor(textItems, '#000');
}