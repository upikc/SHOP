import unittest
from datetime import datetime

import DataContex

class Test(unittest.TestCase):
    def getTimeTest_Equal(self):
        self.assertEqual(DataContex.datetime, str(datetime.now().strftime("%m/%d/%Y, %H:%M:%S")))




if __name__ == '__main__':
    unittest.main()
