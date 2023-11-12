<script lang="ts">
import {defineComponent, StyleValue} from 'vue'
import {useGameStore} from "@/store";
import FontAwesomeBtn from "@/components/Buttons/FontAwesomeBtn.vue";
import VFontAwesomeBtn from "@/components/Buttons/VFontAwesomeBtn.vue";

export default defineComponent({
  name: "LoadSaveGameDialog",
  components: {VFontAwesomeBtn, FontAwesomeBtn},
  setup(){
    const colorStore = useColorStore()
    const gameStore = useGameStore()
    const toast = useToast()
    return {getColorStore: colorStore, gameStore, toast}
  },
  props: {
    visible: {
      type: Boolean,
      default: false,
    }
  },
  watch: {
    uploadedGameState(gameState: File[])
    {
      if(gameState.length !== 0)
      {
        this.fileUploaded = true
        gameState[0].text().then((fileContent) => {

          if(!this.gameStore.loadGame(fileContent))
          {
            this.toast.error(this.$t('toasts.error.gamestate_invalid'))
            this.fileUploaded = false
            this.uploadedGameState = [];
          }
          else
          {
            this.toast.success(this.$t('toasts.success.gamestate_valid'))
            this.$router.replace("/game")
          }
        })
      }
    }
  },
  data() {
    return {
      loadFromFile: false,
      loadFromServer: false,
      fileUploaded: false,
      uploadedGameState: [] as Array<File>
    }
  },
  methods: {
    getColor(color: string = "base"){
      return this.getColorStore.currentColor[color]

    },
    updateVisible() {
      this.loadFromFile = false
      this.loadFromServer = false
      this.fileUploaded = false
      this.$emit('updateVisible', false)
    },
    loadLocal()
    {
      this.loadFromFile = true
    },
    loadRemote()
    {
      // TODO
      this.loadFromServer = true
      this.toast.warning("HALLO :)")
    },
    goBack()
    {
      this.loadFromFile = false
      this.loadFromServer = false
      this.fileUploaded = false
    },

  }

})
</script>

<template>
  <v-dialog
      width="500"
      v-model="visible"
      :persistent="true"
  >
    <v-card>
      <v-card-title>
        <v-row>
          <v-col
              cols="10"
          >
            {{ $t('load_dialog.title')}}
          </v-col>
          <v-col
            cols="2"
            class="ml-dialog-title-btn-group"
          >
            <v-tooltip
                v-if="loadFromFile"
                :text="$t('load_dialog.tooltips.back_btn')"
                location="left"
            >
              <template v-slot:activator="{ props }">
                <font-awesome-btn
                    class="me-4"
                    v-bind="props"
                    :icon="['fas', 'fa-arrow-left']"
                    @click="goBack()"
                />
              </template>
            </v-tooltip>

            <font-awesome-btn

                :icon="['fas', 'fa-close']"
                @click="updateVisible()"
            />
          </v-col>
        </v-row>
      </v-card-title>
      <v-card-text v-if="loadFromFile">
        <v-overlay
            v-model="fileUploaded"
            :contained="true"
            class="align-center justify-center"
        >
          <v-progress-circular
            indeterminate
            size="70"
            width="3"
            :color="getColor('darken1')"
          />
        </v-overlay>

        <v-file-input
            variant="outlined"
            :label="$t('load_dialog.upload_gamestate')"
            prepend-icon=""
            :show-size="true"
            :counter="true"
            :chips="true"
            accept=".aw"
            compact
            v-model="uploadedGameState"
        >
          <template v-slot:append-inner>
            <font-awesome-icon :icon="['fas', 'paperclip']" size="lg"/>
          </template>
        </v-file-input>
      </v-card-text>
      <v-card-text v-else-if="loadFromServer">
      </v-card-text>
      <v-card-text class="ml-dialog-text" v-else>
        {{ $t("load_dialog.description") }}
      </v-card-text>
      <v-card-actions
          v-if="!loadFromFile && !loadFromServer"
          class="ml-dialog-actions evenly"
      >
        <v-font-awesome-btn
            :btn-color="getColor()"
            btn-variant="outlined"
            @click="loadLocal()"
            :icon="['fas', 'fa-upload']"
            icon-text-spacing="me-2"
            iconSize="lg"
            :text="$t('load_dialog.load')"
            :tooltip-text="$t('load_dialog.tooltips.load_local')"
            tooltip-location="bottom"
        />
        <v-font-awesome-btn
            :btn-color="getColor()"
            btn-variant="elevated"
            @click="loadRemote()"
            :icon="['fas', 'fa-cloud-download-alt']"
            icon-text-spacing="me-2"
            iconSize="lg"
            :text="$t('load_dialog.load')"
            :tooltip-text="$t('load_dialog.tooltips.load_remote')"
            tooltip-location="bottom"
        />
      </v-card-actions>
      <v-card-actions v-else-if="loadFromServer">

      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<style scoped lang="scss">
@import '@/scss/ml-dialog';



</style>