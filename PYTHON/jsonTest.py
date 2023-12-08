import json
import dataclasses

@dataclasses.dataclass
class Basket:
    Product: str
    Count: int

    @staticmethod
    def deserializer(data: dict | list):
        if isinstance(data, dict):
            return Basket.__create_person(data)
        elif isinstance(data, list):
            basket = []
            for value in data:
                basket.append(Basket.__create_person(value))
            return basket
        else:
            raise Exception("Exception")

    @staticmethod
    def __create_person(data: dict):
        #data['Product'] = Product(**data['Product'])
        print(json.loads(data))
        return json.loads(data)


def main():


    basket1 = Basket("text1", 11)
    basket2 = Basket("text2", 22)

    lst = [basket1, basket2]

    print(lst)
    print(json.dumps(lst,default=lambda x:x.__dict__))


    data_json = '[{"Product": "text1", "Count": 11}, {"Product": "text2", "Count": 22}]'
    data_json = json.loads(data_json)


    for i in data_json:
        print(i)







if __name__ == '__main__':
    main()
