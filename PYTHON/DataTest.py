
import unittest

def add(a, b):
    return a + b
class TestAddFunction(unittest.TestCase):
    def test_positive_numbers(self):
        self.assertEqual(add(2, 3), 5)


    def test_negative_numbers(self):
        self.assertEqual(add(-2, -3), -5)



if __name__ == '__main__':
    unittest.main()