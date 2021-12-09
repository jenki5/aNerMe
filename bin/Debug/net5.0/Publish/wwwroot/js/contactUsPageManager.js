const submitBtn = document.getElementById('submit-page-changes');
const opens = document.querySelectorAll('.open');
const closes = document.querySelectorAll('.close');
const deliveryMethods = document.querySelectorAll('.delivery-method');
const menuItemID = document.getElementById('menu-item-id');
const storeName = document.getElementById('store-name');
const streetAddress = document.getElementById('street-address');
const city = document.getElementById('city');
const state = document.getElementById('state');
const zip = document.getElementById('zip');
const phoneNumber = document.getElementById('phone-number');
const sundayOpen = document.getElementById('sunday-open');
const mondayOpen = document.getElementById('monday-open');
const tuesdayOpen = document.getElementById('tuesday-open');
const wednesdayOpen = document.getElementById('wednesday-open');
const thursdayOpen = document.getElementById('thursday-open');
const fridayOpen = document.getElementById('friday-open');
const saturdayOpen = document.getElementById('saturday-open');
const sundayClose = document.getElementById('sunday-close');
const mondayClose = document.getElementById('monday-close');
const tuesdayClose = document.getElementById('tuesday-close');
const wednesdayClose = document.getElementById('wednesday-close');
const thursdayClose = document.getElementById('thursday-close');
const fridayClose = document.getElementById('friday-close');
const saturdayClose = document.getElementById('saturday-close');

var contactUsPageErrors = 0;

submitBtn.addEventListener('click', (e) => {
    e.preventDefault();

    countContactUsPageErrors();
    if(contactUsPageErrors == 0){
        submitContactUsPage();
    }
    else{
        alert("Please correct the following errors before submitting");
        contactUsPageErrors = 0;
    }
})

function countContactUsPageErrors(){    
    for(let i = 0; i < opens.length; i++){
        console.log(opens[i].value, closes[i].value);
        if(opens[i].value > closes[i].value){
            opens[i].style.borderColor = "red";
            closes[i].style.borderColor = "red";
            contactUsPageErrors++;
        }
    }
    if(storeName.value == ""){
        storeName.style.borderColor = "red";
        contactUsPageErrors++;
    }
    if(phoneNumber.value == ""){
        phoneNumber.style.borderColor = "red";
        contactUsPageErrors++;
    }
    if(streetAddress.value == ""){
        streetAddress.style.borderColor = "red";
        contactUsPageErrors++;
    }
    if(city.value == ""){
        city.style.borderColor = "red";
        contactUsPageErrors++;
    }
    if(state.value == ""){
        state.style.borderColor = "red";
        contactUsPageErrors++;
    }
    if(zip.value == ""){
        zip.style.borderColor = "red";
        contactUsPageErrors++;
    }
}

function submitContactUsPage(){
    const newCUP = new ContactUsPage();
    const image = document.querySelector('.selected-image');
    console.log(image);
    if(image != null){
        newCUP.ImageID = image.id.substring(image.id.lastIndexOf('-') + 1);
    }
    newCUP.Address = new Address(streetAddress.value, city.value, state.value, zip.value)
    newCUP.StoreName = storeName.value;
    newCUP.PhoneNumber = phoneNumber.value;
    newCUP.SundayOpen = sundayOpen.value;
    newCUP.MondayOpen = mondayOpen.value;
    newCUP.TuesdayOpen = tuesdayOpen.value;
    newCUP.WednesdayOpen = wednesdayOpen.value;
    newCUP.ThursdayOpen = thursdayOpen.value;
    newCUP.FridayOpen = fridayOpen.value;
    newCUP.SaturdayOpen = saturdayOpen.value;
    newCUP.SundayClose = sundayClose.value;
    newCUP.MondayClose = mondayClose.value;
    newCUP.TuesdayClose = tuesdayClose.value;
    newCUP.WednesdayClose = wednesdayClose.value;
    newCUP.ThursdayClose = thursdayClose.value;
    newCUP.FridayClose = fridayClose.value;
    newCUP.SaturdayClose = saturdayClose.value;

    for(let i = 0; i < deliveryMethods.length; i++){
        if(deliveryMethods[i].checked){
            newCUP.StoreDeliveryMethods.push(new StoreDeliveryMethod(deliveryMethods[i].value))
        }
    }

    console.log(newCUP, menuItemID.value);

    $.ajax({
        type: "POST",
        url: '/Business/SubmitContactUsPage',
        dataType: "json",
        data: { CUP: newCUP, MenuItemID: menuItemID.value },
        success: function (Message) {
            document.location.reload(true);
        },
        error: function () {
            alert("Save Unsuccessful: Please check your internet connection and submit again.");
        }
    });
}

class ContactUsPage{
    constructor(){
        this.ImageID;
        this.StoreName;
        this.PhoneNumber;
        this.Address;
        this.SundayOpen;
        this.MondayOpen;
        this.TuesdayOpen;
        this.WednesdayOpen;
        this.ThursdayOpen;
        this.FridaydayOpen;
        this.SundayClose;
        this.MondayClose;
        this.TuesdayClose;
        this.WednesdayClose;
        this.ThursdayClose;
        this.FridaydayClose;
        this.StoreDeliveryMethods = [];
    }
}

class Address{
    constructor(streetAddress, city, state, zip){
        this.StreetAddress = new StreetAddress(streetAddress);
        this.City = new City(city);
        this.State = new State(state);
        this.Zip = zip;
    }
}

class StreetAddress{
    constructor(streetAddress){
        this.StreetAddressName = streetAddress;
    }
}

class City{
    constructor(city){
        this.CityName = city;
    }
}

class State{
    constructor(state){
        this.StateName = state;
    }
}

class StoreDeliveryMethod{
    constructor(deliveryMethodID){
        this.DeliveryMethodID = deliveryMethodID;
    }
}

