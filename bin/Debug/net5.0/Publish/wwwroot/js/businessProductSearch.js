const productContainer = document.getElementById('products-container');
const search = document.getElementById('search');

search.addEventListener('keyup', () => {
    searchProducts(search.value);
});

for(let i = 0; i < dbCompany.products.length; i++)
{
    productContainer.innerHTML += createProductString(i);
}

function searchProducts(phrase){
    productContainer.innerHTML = "";
    let count = 0
    for(let i = 0; i < dbCompany.products.length; i++){
        if(dbCompany.products[i].productTitle.toLowerCase().includes(phrase.toLowerCase()) || dbCompany.products[i].detailedDesc.toLowerCase().includes(phrase.toLowerCase())){
            productContainer.innerHTML += createProductString(i);
            count++;
            if(count > 10){
                break;
            }
        }
    }
}

function createProductString(dbIndex){
    let product = `
        <div class="review">
            <div class="product borderless">
                <div class="product-header">
                    <h2>${dbCompany.products[dbIndex].productTitle}</h2>
                    <span><b>Price: </b>$${dbCompany.products[dbIndex].storePrice}</span>
                    <span><b>Inventory: </b>${dbCompany.products[dbIndex].inventory}</span>
                    <a class="btn btn-primary" href="/EditProduct/${dbCompany.products[dbIndex].productID}">Edit Product</a>
                </div>
                <div class="product-desc">
                    <div class="img-div">
    `;
    console.log(dbCompany.products[dbIndex]);
    if(dbCompany.products[dbIndex].productImages.length > 0 && dbCompany.products[dbIndex].productImages != undefined)
    {
        product += `
            <img src="${dbCompany.products[dbIndex].productImages[0].image.imagePath}" alt="">
        `;
    }
    product += `        
                    </div>
                    <div class="descriptions">
                        <p><b>High Level Description:</b> ${dbCompany.products[dbIndex].highLevelDesc}</p>
                        <p><b>Detailed Description:</b> ${dbCompany.products[dbIndex].detailedDesc}</p>
                    </div>
                </div>
                <div class="categories-div">
                    <div class="typed-categories">
        `;

        for(let x = 0; x < dbCompany.products[dbIndex].productCategories.length; x++)
        {
            product += `
                <div class="category">${dbCompany.Products[dbIndex].ProductCategories[x].Category.CategoryName}</div>
            `;
        }

    product += `
                    </div>
                </div>
            </div>
        </div>
    `;

    return product;
}