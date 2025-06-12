import { createSystem, defaultConfig, defineConfig } from "@chakra-ui/react";
import { colors } from "./tokens/colors";
import { fonts } from "./tokens/fonts";

const customConfig = defineConfig({
  config: {
    initialColorMode: "light",
    useSystemColorMode: false,
  },
  theme: {
    tokens: {
      colors,
      fonts,
    },
    semanticTokens: {
      colors: {
        text: {
          default: { value: "{colors.neutral.900}" },
          _dark: { value: "{colors.neutral.100}" },
        },
        bg: {
          default: { value: "{colors.neutral.100}" },
          _dark: { value: "{colors.neutral.900}" },
        },
        primary: {
          default: { value: "{colors.brand.500}" },
        },
        danger: {
          default: { value: "{colors.danger.500}" },
        },
      },
    },
  },
});

export const system = createSystem(defaultConfig, customConfig);
