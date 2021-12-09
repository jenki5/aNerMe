const respondLinks = document.querySelectorAll(".respond");

respondLinks.forEach(link => {
    link.addEventListener('click', () => {
        if(link.nextElementSibling.style.display != 'block'){
            link.nextElementSibling.style.display = 'block';
            link.innerText = "Hide";
        }
        else{
            link.nextElementSibling.style.display = 'none';
            link.innerText = "Respond";
        }
    })
})