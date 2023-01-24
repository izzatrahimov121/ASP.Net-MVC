var loadmore = document.getElementById("loadmore");

loadmore.addEventListener("click", function () {
    fetch("/Home/LoadMore").then(data => data.text()).then(response => console.log(response))
})