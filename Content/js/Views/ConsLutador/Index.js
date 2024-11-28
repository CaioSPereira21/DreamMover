var appConsLutador = new Vue({
    el: "#consLutadorApp",
    data: {
        Lutadores: [],
        Lutador: '',
        NomeRival: '',
    },
    methods: {
        getLutadores: function () {
            var self = this;

            $.ajax({
                type: "POST",
                url: "/ConsLutador/GetLutadores",
                success: function (data) {
                    self.Lutadores = data.lutadores;
                },
                error: function () {

                }
            });
        },
        getDetalhesLutador: function (lutador) {
            var self = this;

            self.NomeRival = "";

            $.ajax({
                type: "POST",
                url: "/ConsLutador/GetDetalhesLutador",
                data: { lutador },
                success: function (data) {
                    self.Lutador = data.lutador;
                },
                error: function () {

                }
            });

            $('#modal-detalhes').modal('show');
        },
    },
    mounted: function () {
        var self = this;

        self.getLutadores();
    },
    components: {

    },
    computed: {
        RivaisFiltrados() {
            if (this.Lutador) {
                return this.Lutador.Rivais.filter(rival => {
                    return rival.Nome.toLowerCase().includes(this.NomeRival.toLowerCase());
                });
            } else
                return [];
        }
    },
    watch: {
    },
});