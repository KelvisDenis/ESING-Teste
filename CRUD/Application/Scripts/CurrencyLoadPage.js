function formatCurrency(value) {
    // Verifica se o valor é um número válido
    if (isNaN(value) || value === null || value === "") {
        return "R$ 0,00"; // Retorna 0 caso o valor seja inválido
    }

    // Converte o valor para número
    let numericValue = parseFloat(value);

    // Formata o número como moeda em reais
    if (numericValue >= 10000) {
        return (numericValue / 100).toLocaleString('pt-BR', {
            style: 'currency',
            currency: 'BRL'
        });
    } else {
        // Para valores abaixo de R$ 100,00, pode ser que você queira mantê-los em centavos
        return numericValue.toLocaleString('pt-BR', {
            style: 'currency',
            currency: 'BRL'
        });
    }
}

function applyCurrencyFormatting() {
    let salaryElements = document.querySelectorAll('.salary-value');

    salaryElements.forEach(function (element) {
        let rawValue = element.getAttribute('data-raw');
        element.innerText = formatCurrency(rawValue);
    });
}

window.onload = function () {
    applyCurrencyFormatting();
};
