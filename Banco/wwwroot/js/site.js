var site = function () {

	var sacar = function () {

		var valor = $('#valorSaque').val();
		var conta_id = $('#conta_id').val();

		$.ajax({
			url: "/Home/Sacar?valor=" + valor + "&id=" + conta_id,
			method: "GET",
			success: function (result) {
				if (result == 'Saldo Insuficiente') {
					alert(result);
				} else {
					$('#valorSaldo').html(result.saldo);
					alert("saque realizado com sucesso");
				}
			}
		});		
	}

	var depositar = function () {

		var valor = $('#valorDeposito').val();
		var conta_id = $('#conta_id').val();

		$.ajax({
			url: "/Home/Depositar?valor=" + valor + "&id=" + conta_id,
			method: "GET",
			success: function (result) {
				if (result == 'O valor deve ser maior que 0') {
					alert(result);
				} else {
					$('#valorSaldo').html('Saldo: ' + result.saldo);
					alert("Deposito realizado com sucesso");
				}
			}
		});

	}

	var transferir = function () {

		var valor = $('#valorTransferencia').val();
		var agencia = $('#clienteAgencia').val();
		var conta = $('#clienteConta').val();
		var conta_id = $('#conta_id').val();

		$.ajax({
			url: "/Home/Transferir?valor=" + valor + "&id=" + conta_id + "&agencia=" + agencia + "&conta=" + conta,
			method: "GET",
			success: function (result) {
				if (result == 'Saldo Insuficiente' || result == 'agência e/ou conta inválido') {
					alert(result);
				} else {
					$('#valorSaldo').html('Saldo: ' + result.saldo);
					alert("Transferência realizada com sucesso");
				}
			}
		});

	}

	return {
		sacar: sacar,
		depositar: depositar,
		transferir: transferir
	}

}();
