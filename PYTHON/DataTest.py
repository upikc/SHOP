import json
import unittest
from datetime import datetime
from DataContex import DataContex

class Test(unittest.TestCase):
    def test_time(self):
        self.assertEqual(DataContex.NowTime()[:-2], str(datetime.now().strftime("%m/%d/%Y, %H:%M:%S"))[:-2])
    def test_deserialize_dict(self):
        with open("testTXTfile.txt", "r", encoding="UTF-8") as f:
            prodList = json.loads(f.readlines()[1]) #берем лист товаров из файла
            self.assertIsInstance(prodList, list)
    def test_deserialize_list(self):
        with open("testTXTfile.txt", "r", encoding="UTF-8") as f:
            prodList = json.loads(f.readlines()[0])[0] ##берем один товар из файла
            self.assertIsInstance(prodList, dict)

if __name__ == '__main__':
    unittest.main()





