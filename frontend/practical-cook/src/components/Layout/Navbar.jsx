import { NavLink } from "react-router-dom";
import logo from "../../assets/PC.png";
import {
  Avatar,
  Box,
  Drawer,
  Flex,
  HStack,
  Icon,
  IconButton,
  Image,
  Spacer,
  Stack,
  Text,
} from "@chakra-ui/react";
import { HiMenu, HiX } from "react-icons/hi";
import { useState } from "react";

const Links = [
  { name: "Recipes", path: "/" },
  { name: "Calendar", path: "/" },
  { name: "Shopping List", path: "/" },
];

export default function Navbar() {
  const [open, setOpen] = useState(false);

  return (
    <Box bg={"brand.700"} px={4} boxShadow="md">
      <Flex h={16} alignItems={"center"} justifyContent={"space-between"}>
        <HStack
          gap={8}
          alignItems="center"
          display={{ base: "none", md: "flex" }}
          w="100%"
        >
          <NavLink to="/">
            <Image
              size="md"
              src={logo}
              alt="Logo"
              boxSize={"65px"}
              fit={"cover"}
            />
          </NavLink>
          {Links.map((link) => (
            <NavLink key={link.name} to={link.path}>
              <Text
                fontSize={"xl"}
                color={"white"}
                _hover={{ textDecoration: "underline" }}
              >
                {link.name}
              </Text>
            </NavLink>
          ))}
          <Spacer />
          <NavLink to="/Auth?mode=login">
            <Avatar.Root colorPalette={"green"} size={"lg"} bg={"neutral.900"}>
              <Avatar.Fallback />
            </Avatar.Root>
          </NavLink>
        </HStack>
        <Drawer.Root open={open} onOpenChange={(e) => setOpen(e.open)}>
          <Drawer.Backdrop />
          <Drawer.Trigger asChild>
            <IconButton
              color={"neutral.100"}
              bg={"brand.900"}
              size="md"
              aria-label="Open Menu"
              display={{ md: "none" }}
            >
              <HiMenu />
            </IconButton>
          </Drawer.Trigger>
          <Drawer.Positioner>
            <Drawer.Content>
              <Drawer.Body>
                <Stack gap={8}>
                  <NavLink to="/Auth?mode=login">
                    <Avatar.Root
                      colorPalette={"green"}
                      size={"lg"}
                      bg={"neutral.900"}
                    >
                      <Avatar.Fallback />
                    </Avatar.Root>
                  </NavLink>
                  {Links.map((link) => (
                    <NavLink key={link.name} to={link.path}>
                      <Text
                        fontSize={"xl"}
                        color={"white"}
                        _hover={{ textDecoration: "underline" }}
                      >
                        {link.name}
                      </Text>
                    </NavLink>
                  ))}
                </Stack>
              </Drawer.Body>
              <Drawer.CloseTrigger asChild>
                <IconButton
                  color={"neutral.100"}
                  bg={"brand.900"}
                  size="md"
                  aria-label="Close Menu"
                >
                  <HiX />
                </IconButton>
              </Drawer.CloseTrigger>
            </Drawer.Content>
          </Drawer.Positioner>
        </Drawer.Root>
      </Flex>
    </Box>
  );
}
