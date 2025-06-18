import RecipeItem from "./RecipeItem";
import { Stack, ButtonGroup, IconButton, Pagination } from "@chakra-ui/react";
import { useState } from "react";
import { HiChevronLeft, HiChevronRight } from "react-icons/hi";
import { motion } from "motion/react";

export function RecipeList({ recipes }) {
  const [page, setPage] = useState(1);
  const pageSize = 4;
  const startRange = (page - 1) * pageSize;
  const endRange = startRange + pageSize;
  const paginatedRecipes = recipes.slice(startRange, endRange);

  return (
    <>
      <motion.div
        key={page}
        initial={{ opacity: 0, x: 20 }}
        animate={{ opacity: 1, x: 0 }}
        transition={{ duration: 0.4, ease: "easeInOut" }}
      >
        <Stack p={4} gap={5} m={5} direction={"horizontal"}>
          {paginatedRecipes.map((recipe) => (
            <RecipeItem key={recipe.id} recipe={recipe} onSelect={() => {}} />
          ))}
        </Stack>
      </motion.div>
      <Pagination.Root
        w={"100vh"}
        bg={"whiteAlpha.900"}
        p={3}
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
            color={"secondary.500"}
            render={(page) => (
              <IconButton
                variant={{
                  base: "ghost",
                  _selected: "outline",
                }}
              >
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
