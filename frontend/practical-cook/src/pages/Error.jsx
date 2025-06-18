import { useRouteError } from "react-router-dom";
import Navbar from "../components/Layout/Navbar";
import { Box, Heading, Text } from "@chakra-ui/react";

function ErrorPage() {
  const error = useRouteError();

  let title = "An error occurred!";
  let message = "Something went wrong.";

  if (error.status === 500) {
    title = "500 - Internal Server Error";
    message = JSON.parse(error.data).message;
  } else if (error.status === 404) {
    title = "404 - Not Found";
    message = "The page you are looking for does not exist.";
  } else if (error.status === 403) {
    title = "403 - Forbidden";
    message = "You do not have permission to access this page.";
  }
  return (
    <>
      <main>
        <Navbar />
        <Box m={5} p={5} alignItems={"center"} bg={"whiteAlpha.900"}>
          <Heading fontSize={"3xl"} color={"neutral.900"}>
            {title}
          </Heading>
          <Text fontSize={"xl"} color={"neutral.900"}>
            {message}
          </Text>
        </Box>
      </main>
    </>
  );
}

export default ErrorPage;
