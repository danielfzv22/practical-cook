import RecipeItem from "./RecipeItem";
import {
  Box,
  SimpleGrid,
  Stack,
  ButtonGroup,
  IconButton,
  Pagination,
} from "@chakra-ui/react";
import { useState } from "react";
import { HiChevronLeft, HiChevronRight } from "react-icons/hi";
import { motion } from "motion/react";

export function RecipeList({ recipes, fallbackText, isLoading, loadingText }) {
  const [page, setPage] = useState(1);
  const pageSize = 4;
  const startRange = (page - 1) * pageSize;
  const endRange = startRange + pageSize;
  const paginatedRecipes = recipes.slice(startRange, endRange);

  return (
    <>
      <motion.div
        key={page}
        initial={{ opacity: 0, y: 20 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.4, ease: "easeOut" }}
      >
        <Stack spacing={6} gap={10} margin={10} direction={"horizontal"}>
          {paginatedRecipes.map((recipe) => (
            <RecipeItem key={recipe.id} recipe={recipe} onSelect={() => {}} />
          ))}
        </Stack>
      </motion.div>
      <Pagination.Root
        bg={"whiteAlpha.900"}
        padding={3}
        count={recipes.length}
        pageSize={pageSize}
        page={page}
        onPageChange={(e) => setPage(e.page)}
      >
        <ButtonGroup variant="ghost" size="sm">
          <Pagination.PrevTrigger asChild>
            <IconButton>
              <HiChevronLeft />
            </IconButton>
          </Pagination.PrevTrigger>

          <Pagination.Items
            render={(page) => (
              <IconButton variant={{ base: "ghost", _selected: "outline" }}>
                {page.value}
              </IconButton>
            )}
          />

          <Pagination.NextTrigger asChild>
            <IconButton>
              <HiChevronRight />
            </IconButton>
          </Pagination.NextTrigger>
        </ButtonGroup>
      </Pagination.Root>
    </>
  );
}
