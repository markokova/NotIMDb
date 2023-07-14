export const parseDate = (date) => {
    let parsedDate = new Date(date).toUTCString();
    return parsedDate;
}