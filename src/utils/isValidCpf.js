const isValidCpf = (cpf) => {
    if (cpf.length < 11) return false;

    let sum = 0;
    for (let i = 0, validatorDigit = 10; i < 9; i++, validatorDigit--) {
        sum += parseInt(cpf[i]) * validatorDigit;
    }
    let remainder = (sum * 10) % 11;
    if (remainder % 10 !== parseInt(cpf[9])) return false;

    sum = 0;
    for (let i = 0, validatorDigit = 11; i < 10; i++, validatorDigit--) {
        sum += parseInt(cpf[i]) * validatorDigit;
    }
    remainder = (sum * 10) % 11;
    if (remainder % 10 !== parseInt(cpf[10])) return false;

    return true;
};

export default isValidCpf;
