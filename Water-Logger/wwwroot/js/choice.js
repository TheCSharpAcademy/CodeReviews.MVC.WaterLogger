let form = document.querySelector("#form");
    let convertor = document.querySelector("#convertor");
    let quantity = document.querySelector(".quantity-box");
    let standardMeasure = document.querySelector(".standard-measure");
    let submitbtn = document.querySelector("#submit-button");
    let count = document.querySelector("#measure-count");

    function getOption(){
        let selectItems = document.querySelector("#select-items");
        return selectItems.value;
    }

    convertor.addEventListener('click', (e) => {
        e.preventDefault();
        if (e.target.innerText === "USE STANDARD MEASURE") {
            e.target.innerText = "USE CUSTOM MEASURE";
            quantity.style.display = "none";
            standardMeasure.style.display = "block";

        }
        else {
            e.target.innerText = "USE STANDARD MEASURE";
            quantity.style.display = "block";
            standardMeasure.style.display = "none";

        }
    });

    submitbtn.addEventListener('click', (e) => {
        if(convertor.innerText === "USE CUSTOM MEASURE")
        {
            e.preventDefault();
            let optionValue = getOption();
            document.querySelector("#quantity-input").value = parseFloat(optionValue) * count.value;
            form.submit();
        }
    });