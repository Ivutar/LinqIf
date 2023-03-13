using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
    public static class LinqExtensions
    {

        /// <summary>
        /// Вспомогательный generic-метод для организации условных вызовов методов некоторого объекта <paramref name="obj"/> в fluent-стиле.
        /// Заданная функция <paramref name="conditionalQuery"/> применяется к <paramref name="obj"/> только если <paramref name="obj"/> не равен null и <paramref name="condition"/> равен true;
        /// иначе исходное значение <paramref name="obj"/> возвращается "как есть".
        /// <para>Таким образом, множественные вызовы <see cref="QueryIf"/> могут быть применены к одному объекту один за другим,
        /// порождая последовательность изменений внутреннего состояния объекта (или его заместителя).</para>
        /// </summary>
        /// <typeparam name="T">Тип объекта для применения метода.</typeparam>
        /// <typeparam name="R">Тип возвращаемого значения функции <paramref name="conditionalQuery"/> (должен быть равен типу <typeparamref name="T"/> или являться надмножеством исходного типа).</typeparam>
        /// <param name="obj">Объект для условного применения метода <paramref name="conditionalQuery"/>.</param>
        /// <param name="condition">Флаг: разрешено ли применять <paramref name="conditionalQuery"/>.</param>
        /// <param name="conditionalQuery">Функция, применяемая к <paramref name="obj"/>, и обязанная вернуть либо исходный экземпляр с примененными побочными эффектами, либо экземпляр на замену исходного.</param>
        public static R QueryIf<T, R>(this T obj, bool condition, Func<T, R> conditionalQuery)
            where R : class
            where T : class, R
        {
            if (obj == null || !condition)
                return (R)obj;
            else
                return conditionalQuery(obj);
        }

        /// <summary>
        /// Вспомогательный метод для условного применения <see cref="Queryable.Where{TSource}(IQueryable{TSource}, Expression{Func{TSource, bool}})"/> в цепочке построения <see cref="IQueryable"/>.
        /// </summary>
        /// <typeparam name="T">Тип элемента источника.</typeparam>
        /// <param name="query">Запрос для применения фильтрации.</param>
        /// <param name="condition">Условие применения фильтрации.</param>
        /// <param name="predicate">Предикат фильтрации, который должен быть применен, только если <paramref name="condition"/> равен true.</param>
        /// <returns>В зависимости от <paramref name="condition"/>, либо оригинальный запрос <paramref name="query"/>,
        /// либо запрос с применением предиката фильтрации <paramref name="predicate"/>.</returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return query.QueryIf(condition,
                query => query.Where(predicate));
        }

        /// <summary>
        /// Запросить <see cref="takeCount"/> первых записей, если выполняется условие
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"> Исходный запрос. </param>
        /// <param name="condition"> Условие. </param>
        /// <param name="takeCount"> Количество записей. </param>
        /// <returns> Итоговый запрос. </returns>
        public static IQueryable<T> TakeIf<T>(this IQueryable<T> query, bool condition, int takeCount)
        {
            return query.QueryIf(condition,
                q => q.Take(takeCount));
        }

        /// <summary>
        /// Запросить <see cref="takeCount"/> первых записей усли это количество больше нуля.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"> Исходный запрос. </param>
        /// <param name="condition"> Условие. </param>
        /// <param name="takeCount"> Количество записей. </param>
        /// <returns> Итоговый запрос. </returns>
        public static IQueryable<T> TakeIfGreaterThanZero<T>(this IQueryable<T> query, int takeCount)
        {
            return query.TakeIf(takeCount > 0, takeCount);
        }

        /// <summary>
        /// Пропустить <see cref="skipCount"/> первых записей, если выполняется условие
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"> Исходный запрос. </param>
        /// <param name="condition"> Условие. </param>
        /// <param name="skipCount"> Количество записей. </param>
        /// <returns> Итоговый запрос. </returns>
        public static IQueryable<T> SkipIf<T>(this IQueryable<T> query, bool condition, int skipCount)
        {
            return query.QueryIf(condition,
                q => q.Skip(skipCount));
        }

        /// <summary>
        /// Пропустить <see cref="skipCount"/> первых записей, усли это количество больше нуля.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"> Исходный запрос. </param>
        /// <param name="condition"> Условие. </param>
        /// <param name="takeCount"> Количество записей. </param>
        /// <returns> Итоговый запрос. </returns>
        public static IQueryable<T> SkipIfGreaterThanZero<T>(this IQueryable<T> query, int skipCount)
        {
            return query.SkipIf(skipCount > 0, skipCount);
        }

        /// <summary>
        /// Вспомогательный метод для условного применения <see cref="Queryable.OrderBy{TSource, TKey}(IQueryable{TSource}, Expression{Func{TSource, TKey}})"/> в цепочке построения <see cref="IQueryable"/>.
        /// </summary>
        /// <typeparam name="T">Тип элемента источника.</typeparam>
        /// <typeparam name="TKey">Тип ключа, возвращаемого функцией</typeparam>
        /// <param name="query">Запрос для применения фильтрации.</param>
        /// <param name="condition">Условие применения фильтрации.</param>
        /// <param name="predicate">Предикат фильтрации, который должен быть применен, только если <paramref name="condition"/> не равен false.</param>
        /// <param name="desc">Направление сортировки. По умолчанию false - по убыванию.</param>
        /// <returns>В зависимости от <paramref name="condition"/>, либо оригинальный запрос <paramref name="query"/>,
        /// либо запрос с применением предиката сортировки <paramref name="predicate"/>.</returns>
        public static IQueryable<T> OrderByIf<T, TKey>(this IQueryable<T> query, bool condition, Expression<Func<T, TKey>> predicate, bool desc = false)
        {
            if (condition)
            {
                //TODO: надо понимать что если нужно последовательно применить несколько сортировок, то нужно так же использовать ThenBy и ThenByDescending
                //то есть серия запросов вида entities.OrderByIf(...).OrderByIf(...).OrderByIf(...) применит сортировку из ПОСЛЕДНЕГО вызова метода OrderByIf
                if (desc)
                {
                    return query.OrderByDescending(predicate);
                }
                else
                {
                    return query.OrderBy(predicate);
                }
            }

            return query;
        }

        //public static IQueryable<T> WhereIf<T>(
        //    this IQueryable<T> query,
        //    bool condition,
        //    Expression<Func<T,bool>> predicate
        //) => condition ? query.Where(predicate) : query;

        //public static IEnumerable<T> WhereIf<T>(
        //    this IEnumerable<T> source,
        //    bool condition,
        //    Func<T, bool> predicate
        //) => condition ? source.Where(predicate) : source;
    }
}
