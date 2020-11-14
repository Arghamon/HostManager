const packageSelector = document.getElementById("packageSelector");
const termSelector = document.getElementById("termSelector");
const priceInput = document.getElementById("priceInput");
const editPriceInput = document.getElementById("editPriceInput");
const sslEnabled = document.getElementById("sslCheck");
const sslInput = document.getElementById("sslInput");

Date.prototype.toDateInputValue = (function () {
    var local = new Date(this);
    local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
    return local.toJSON().slice(0, 16);
});

if (packageSelector) {
    packageSelector.addEventListener("change", invokeListener);
    termSelector.addEventListener("change", invokeListener);
    sslEnabled.addEventListener("change", checkSsl);

    $("input[type='datetime-local']").val(new Date().toDateInputValue())
    sslInput.disabled = !Boolean(+sslEnabled.value);
}

if (editPriceInput) {
    const data = {
        PackageId: +packageSelector.value,
        TermId: +termSelector.value,
    }
    GetPrice(data, editPriceInput)
}



function GetPrice(data, element) {
    fetch("/api/price", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(res => res.json())
        .then(data => element.value = data.price)
        .catch(err => console.log(err))
}


function invokeListener() {
    if (packageSelector.value > 0 && termSelector.value > 0) {
        const data = {
            PackageId: +packageSelector.value,
            TermId: +termSelector.value,
        }
        GetPrice(data, priceInput);
    }
}

function checkSsl({ target }) {
    sslInput.disabled = !Boolean(+sslEnabled.value);
}