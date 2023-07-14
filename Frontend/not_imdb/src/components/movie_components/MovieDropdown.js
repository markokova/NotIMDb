import { useState, useEffect } from "react";

function MovieDropdown({ dataSource, valueProperty, displayProperties, dataKey }) {
  const [items, setItems] = useState([]);
  const [selectedItems, setSelectedItems] = useState([]);

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const response = await dataSource();
      setItems(response.data[dataKey]);
    } catch (error) {
      console.error("Error fetching data.", error);
    }
  };

  const handleItemChange = (event) => {
    const selectedOptions = Array.from(event.target.options)
      .filter((option) => option.selected)
      .map((option) => option.value);

    setSelectedItems(selectedOptions);
  };  

  const getDisplayText = (item) => {
    return displayProperties
      .map((property) => item[property])
      .filter((value) => value !== undefined)
      .join(" ");
  };

  return (
    <div>
      <select
        id="dropdown"
        multiple
        value={selectedItems}
        onChange={handleItemChange}
      >
        {items.map((item) => (
          <option key={item[valueProperty]} value={item[valueProperty]}>
            {getDisplayText(item)}
          </option>
        ))}
      </select>
    </div>
  );
}

export default MovieDropdown;
