
<script lang="ts">
import { useTheme } from 'vuetify'
import {useRoute} from "vue-router";
import {i18n} from "@/lang";

export default defineComponent({
  setup()
  {
    const languageStore = useLanguageStore();
    const themeStore = useThemeStore();
    const vuetifyTheme = useTheme()
    return {getLanguageStore: languageStore, getThemeStore: themeStore, getVuetifyTheme: vuetifyTheme}
  },
  head() {
    return {
      title: i18n.global.t('draughts-title')
    }
  },
  components: {
  },
  beforeCreate()
  {
    this.getVuetifyTheme.global.name.value = this.getThemeStore.currentTheme;
    this.$i18n.locale = this.getLanguageStore.currentLanguage;

    const route = useRoute()
    if(route.path === "/game")
    {
      window.onbeforeunload = () => {
        return "";
      }
    }
    else
    {
      window.onbeforeunload = null;
    }


  },

})
</script>


<template>
  <v-toolbar
    :title="$t('draughts-title')"
    density="comfortable"
  >
  </v-toolbar>
  <router-view />
</template>

<style lang="scss" scoped>
</style>
