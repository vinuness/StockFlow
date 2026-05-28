const cadastrar = document.getElementById("cadastrar");

cadastrar.addEventListener("click", (e) => {
    e.preventDefault();

    const usuarioCadastrado = {
        cpf: document.getElementById("cpf").value,
        nome: document.getElementById("nome").value,
        cep: document.getElementById("cep").value,
        estado: document.getElementById("estado").value,
        cidade: document.getElementById("cidade").value,
        bairro: document.getElementById("bairro").value,
        rua: document.getElementById("rua").value,
        email: document.getElementById("email").value,
        senha: document.getElementById("senha").value
    };

    if (
        usuarioCadastrado.cpf &&
        usuarioCadastrado.nome &&
        usuarioCadastrado.email &&
        usuarioCadastrado.senha
    ) {
        alert("Cadastro efetuado com sucesso");
        console.log(usuarioCadastrado);
    } else {
        alert("Preencha os campos obrigatórios");
    }
});