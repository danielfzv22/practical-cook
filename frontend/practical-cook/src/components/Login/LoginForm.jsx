import {
  Box,
  Button,
  Field,
  Flex,
  Heading,
  Input,
  Stack,
  Text,
  Link as ChakraLink,
  List,
} from "@chakra-ui/react";
import { PasswordInput } from "../UI/password-input.jsx";
import {
  Form,
  Link,
  useActionData,
  useNavigation,
  useSearchParams,
} from "react-router-dom";

function LoginForm() {
  const data = useActionData();

  const navigation = useNavigation();
  const isSubmitting = navigation.state === "submitting";

  const [searchParams] = useSearchParams();
  const mode = searchParams.get("mode") || "login";
  const isLogin = mode === "login";

  return (
    <Flex minH="80vh" align="center" justify="center">
      <Box
        bg="neutral.100"
        p={8}
        rounded="md"
        shadow="md"
        w={{ base: "90%", sm: "400px" }}
      >
        <Heading p={2} size="6xl" textAlign="center" color="brand.900">
          {isLogin ? "Welcome Back !" : "Create a New Account"}
        </Heading>
        <Form method="post">
          <Stack gap="4" maxW="sm" css={{ "--field-label-width": "96px" }}>
            {data && !data.success && (
              <Text color="accent.900">Error: {data.message}</Text>
            )}
            <Field.Root>
              <Field.Label fontSize="md" color="neutral.900">
                Email
                <Field.RequiredIndicator />
              </Field.Label>
              <Input
                fontFamily="system-ui, sans-serif"
                fontSize="lg"
                color="neutral.900"
                id="email"
                name="email"
                type="email"
                required
              />
            </Field.Root>
            <Field.Root>
              <Field.Label fontSize="md" color="neutral.900">
                Password
              </Field.Label>
              <PasswordInput
                fontFamily="system-ui, sans-serif"
                fontSize="lg"
                color="neutral.900"
                size="lg"
                id="password"
                type="password"
                name="password"
                required
              />
            </Field.Root>
            <Button
              fontFamily={"body"}
              fontSize="lg"
              bg={`${isLogin ? "neutral.900" : "accent.900"}`}
              color="neutral.100"
              size="lg"
              _hover={{
                bg: "accent",
                color: "neutral.100",
              }}
              disabled={isSubmitting}
              type="submit"
            >
              {isLogin ? "Login" : "Create Account"}
            </Button>
            <Text fontSize="md" color="neutral.900" textAlign="center">
              {isLogin ? "Don't have an account?" : "Already have an account?"}
              <br />
              <ChakraLink asChild color="accent.500">
                <Link to={`?mode=${isLogin ? "signup" : "login"}`}>
                  {isLogin ? "Sign Up!" : "Login!"}
                </Link>
              </ChakraLink>
            </Text>
          </Stack>
        </Form>
      </Box>
    </Flex>
  );
}

export default LoginForm;
