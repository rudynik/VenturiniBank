var site = function () {

	var sacar = function () {

		var valor = $('#valorSaque').val();
		var conta_id = $('#conta_id').val();

		$.ajax({
			url: "/Home/Sacar?valor=" + valor + "&id=" + conta_id,
			method: "GET",
			data: conta,
			success: function (result) {
				if (result.data) {
					alert('diminui o saldo');
				} else {
					alert('avisa o cliente');
				}
			}
		});

		
	}

	return {
		sacar: sacar
	}

}();
