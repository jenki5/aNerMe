const street = document.getElementById('street');
const city = document.getElementById('city');
const state = document.getElementById('state');
const zip = document.getElementById('zip');
const submitbtn = document.getElementById('submit-address');
const addressTable = document.getElementById('address-table');

submitbtn.addEventListener('click', (e) => {
    e.preventDefault();
    let addressLine = `
    <tr>
        <td>${street.value}</td>
        <td>${city.value}</td>
        <td>${state.value}</td>
        <td>${zip.value}</td>
    </tr>
    `;
    addressTable.innerHTML += addressLine;
    clearInputs();
})

function clearInputs(){
    street.value = "";
    city.value = "";
    state.value = "";
    zip.value = "";
}