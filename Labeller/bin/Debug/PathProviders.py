import numpy as np
import os

def random_path(start_path):


    patient = None

    patient = os.listdir(start_path)

    leng = len([f for f in patient if not os.path.isfile(os.path.join(start_path, f))])

    patient_path = patient[np.random.randint(0,leng)]

    date = os.listdir(start_path+"/"+patient_path+"/")

    date_path = date[np.random.randint(0,len(date))]

    if start_path=='':
        return patient_path+"/"+date_path+"/"
    else:
        return start_path+"/"+patient_path + "/" + date_path + "/"


def getUnusedPath(start_path, i = 0):
    if(i>100):
        return None
    path = random_path(start_path)
    if os.path.isfile(path+'info/data.csv'):
        return getUnusedPath(start_path, i+1)
    else:
        return path