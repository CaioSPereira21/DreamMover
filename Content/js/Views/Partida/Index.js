Vue.component('multiselect', VueMultiselect.default);

var appPartida = new Vue({
    el: "#partidaApp",
    data: {
        Lutadores: [],
        LutadorCasa: '',
        LutadorVisitante: '',
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
        selecionarLutador(lutador) {
            var self = this;

            if (!self.LutadorCasa)
                self.LutadorCasa = lutador;
            else if (!self.LutadorVisitante)
                self.LutadorVisitante = lutador;
        },
        iniciarPartida() {
            var self = this;

            var lutadorCasa = self.LutadorCasa;
            var lutadorVisitante = self.LutadorVisitante;

            $.ajax({
                type: "POST",
                url: "/Partida/IniciarPartida",
                data: { lutadorCasa, lutadorVisitante },
                success: function (data) {

                },
                error: function () {

                }
            });
        }
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