function formatCurrency(input) {
    var value = input.value.replace(/\D/g, '');

    value = (value / 100).toFixed(2);
    value = value.replace('.', ',');

    input.value = 'R$ ' + value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.');
}


