namespace Domain.Service
{
    public static class CategoriesServiceStrings
    {
        private const string BaseInternalException = "Internal error occured while ";

        private const string BaseUpdateCategoryException = "Failed to update category. ";

        private const string BaseAddCategoryException = "Failed to add category. ";

        public const string UpdateCategoryException = BaseInternalException + "updating category";

        public const string DeleteCategoryException = BaseInternalException + "deleting category";

        public const string AddCategoryException = BaseInternalException + "adding category";

        public const string GetCategoriesException = BaseInternalException + "getting categories";

        public const string GetCategoryException = BaseInternalException + "getting category";

        public const string UpdateCategoryNullException = BaseUpdateCategoryException + "Category is null";

        public const string UpdateCategoryIdException = BaseUpdateCategoryException + "Missing id";

        public const string UpdateCategoryQuestionTitleException =
            BaseUpdateCategoryException + "Missing question title";

        public const string UpdateCategoryQuestionIdException = BaseUpdateCategoryException + "Missing question id";

        public const string UpdateCategoryOptionTitleException = BaseUpdateCategoryException + "Missing option title";

        public const string UpdateCategoryOptionIdException = BaseUpdateCategoryException + "Missing option Id";

        public const string UpdateCategoryTitleException = BaseUpdateCategoryException + "Missing title";

        public const string UpdateCategoryDateException = BaseUpdateCategoryException + "Missing date of creation";

        public const string AddCategoryNullException = BaseAddCategoryException + "Category is null";

        public const string AddCategoryQuestionTitleException = BaseAddCategoryException + "Missing question title";

        public const string AddCategoryOptionTitleException = BaseAddCategoryException + "Missing option title";

        public const string AddCategoryTitleException = BaseAddCategoryException + "Missing title";

        public const string AddCategorySameTitleException =
            BaseAddCategoryException + "Category with this title already exists";

        public const string AddCategoryUserIdException = BaseAddCategoryException + "Missing user id";

        public const string AddCategoryDateException = BaseAddCategoryException + "Missing date of creation";

        public const string DeleteCategoryIdException = "Failed to delete category. Missing id";

        public const string GetCategoryIdException = "Failed to get category. Wrong id";

        public const string GetCategoriesNullException = "Failed to get categories";
    }
}