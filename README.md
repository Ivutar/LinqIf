# LinqIf

        private async Task<List<FormatDtoGraphQl>> GetRowsAsync(FormatFilter filter, FormatOrder orderBy, int? offset = null, int? limit = null)
        {
            var query = _dbSet.AsNoTracking()
                .Where(s => !s.IsDeleted)
                .WhereIf(filter.ContentHeightMax.HasValue, format => format.ContentHeight <= filter.ContentHeightMax.Value)
                .WhereIf(filter.ContentHeightMin.HasValue, format => format.ContentHeight >= filter.ContentHeightMin.Value)
                .WhereIf(filter.ContentWidthMax.HasValue, format => format.ContentWidth <= filter.ContentWidthMax.Value)
                .WhereIf(filter.ContentWidthMin.HasValue, format => format.ContentWidth >= filter.ContentWidthMin.Value)
                .WhereIf(filter.FormatType.HasValue, format => format.FormatType == filter.FormatType.Value)
                .WhereIf(filter.InvariantNames != null, format => filter.InvariantNames.Any(f => EF.Functions.Like(format.InvariantName, SQLHelper.EscapeForLIKE(f))))
                .OrderByIf(orderBy.ContentHeight, format => format.ContentHeight)
                .OrderByIf(orderBy.InvariantName, format => format.InvariantName)
                .OrderByIf(orderBy.MeasureInvariantName, format => format.Measure.InvariantName)
                .OrderByIf(orderBy.ContentWidth, format => format.ContentWidth)
                .OrderByIf(orderBy.FormatType, format => format.FormatType)
                .OrderByIf(orderBy.Fps, format => format.Fps)
                .OrderByIf(orderBy.Id, format => format.Id);

            if (offset.HasValue && limit.HasValue)
                query = query.Skip(offset.Value).Take(limit.Value);
            
            var dtoList = await query.ProjectTo<FormatDtoGraphQl>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (dtoList?.Count == 0)
                return null;

            var translations =
                await GetTranslationsForRecordsAsync(RefTableKind.Format,
                    dtoList.Select(i => ((IConvertible)i.Id).ToInt64(null)).ToArray()
                );

            dtoList.ForEach(c =>
                c.Translations = translations
                    .Where(t => t.RefRecordId == ((IConvertible)c.Id).ToInt64(null))
                    .ToList());

            return dtoList;
        }

