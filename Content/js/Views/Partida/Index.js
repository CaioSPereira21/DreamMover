Vue.component('multiselect', VueMultiselect.default);

var appPartida = new Vue({
    el: "#partidaApp",
    data: {
        Lutadores: [],
        LutadorCasa: '',
        LutadorVisitante: '',
        LutadorCasaManual: false,
        LutadorVisitanteManual: false,
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

            if (!self.LutadorCasa) {
                self.LutadorCasa = lutador;
                self.LutadorCasaManual = true;
            }
            else if (!self.LutadorVisitante) {
                self.LutadorVisitante = lutador;
                self.LutadorVisitanteManual = true;
            }

        },
        limparSelecao() {
            var self = this;

            self.LutadorCasa = '';
            self.LutadorCasaManual = false;
            self.LutadorVisitante = '';
            self.LutadorVisitanteManual = false;
        },
        selecaoAleatoria() {
            var self = this;

            // Gera um índice aleatório para o primeiro lutador
            const indiceCasa = Math.floor(Math.random() * self.Lutadores.length);

            let indiceVisitante;

            // Gera o índice do visitante garantindo que seja diferente do índiceCasa
            do {
                indiceVisitante = Math.floor(Math.random() * self.Lutadores.length);
            } while (indiceVisitante === indiceCasa);

            // Atribui os lutadores às variáveis
            if (self.LutadorCasaManual == false)
                self.LutadorCasa = self.Lutadores[indiceCasa];

            if (self.LutadorVisitanteManual == false)
                self.LutadorVisitante = self.Lutadores[indiceVisitante];
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