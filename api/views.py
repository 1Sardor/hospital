from rest_framework.decorators import action, api_view
from django.contrib.auth import authenticate
from rest_framework.authentication import TokenAuthentication
from rest_framework.authtoken.models import Token
from rest_framework.response import Response
from rest_framework import viewsets
from .serializer import *
from main.models import *
from rest_framework.permissions import IsAuthenticated
from django.db.models import Q
from api.authentication import MyOwnTokenAuthentication


@api_view(['POST'])
def Login(request):
    try:
        username = request.data['username']
        password = request.data['password']
        try:
            usrs = Doctor.objects.get(username=username)
            user = authenticate(username=username, password=password)
            if user is not None:
                status = 200
                token, created = Token.objects.get_or_create(user=user)
                data = {
                    'status': status,
                    'username': username,
                    'user_id': usrs.id,
                    'token': token.key,
                    'phone': usrs.phone,
                    'direction': usrs.direction.name,
                    'hospital': usrs.hospital.name,
                }
            else:
                status = 403
                message = 'Username yoki parol xato!'
                data = {
                    'status': status,
                    'message': message,
                }
        except Doctor.DoesNotExist:
            status = 404
            message = 'Bunday foydalanuvchi mavjud emas!'
            data = {
                'status': status,
                'message': message,
            }
        return Response(data)
    except Exception as er:
        return Response({"error": f'{er}'})


@api_view(['POST'])
def Pharmacylogin(request):
    username = request.data['username']
    password = request.data['password']
    try:
        cl = Pharmacy.objects.get(login=username)
        if password == cl.password:
            status = 200
            try:
                myT = MyOwnToken.objects.get(user_id=cl.id)
            except MyOwnToken.DoesNotExist:
                myT = MyOwnToken.objects.create(user_id=cl.id)

            data = {
                'status': status,
                'pharmacy': cl.id,
                'token': myT.key,
                'fio': cl.username,
            }
        else:
            status = 403
            massage = "Username yoki parol xato!"
            data = {
                'status': status,
                'massage': massage,
            }
    except Pharmacy.DoesNotExist:
        status = 404
        massage = "Bunday foydalanuvchi mavjud emas!"
        data = {
            'status': status,
            'massage': massage,
        }

    return Response(data)


class DoctorView(viewsets.ModelViewSet):
    queryset = Doctor.objects.all()
    serializer_class = DoctorSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    def create(self, request):
        try:
            username = request.data['username']
            password = request.data['password']
            direction_id = request.data['direction']
            hospital_id = request.data['hospital']
            phone = request.data['phone']
            user = Doctor.objects.create_user(username=username, password=password, phone=phone, direction_id=direction_id, hospital_id=hospital_id,)
            ser = self.serializer_class(user)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})

    @action(methods=['GET'], detail=False)
    def get_hospital_doctors(self, request):
        try:
            hospital_id = request.data['hospital']
            query = self.queryset.filter(hospital_id=hospital_id)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class DirectionView(viewsets.ModelViewSet):
    queryset = Direction.objects.all()
    serializer_class = DirectionSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)


class ProvinceView(viewsets.ModelViewSet):
    queryset = Province.objects.all()
    serializer_class = ProvinceSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)


class RegionView(viewsets.ModelViewSet):
    queryset = Region.objects.all()
    serializer_class = RegionSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    @action(methods=['GET'], detail=False)
    def get_province_region(self, request):
        try:
            province_id = request.data['province']
            query = self.queryset.filter(province_id=province_id)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class HospitalView(viewsets.ModelViewSet):
    queryset = Hospital.objects.all()
    serializer_class = HospitalSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)


class PatientView(viewsets.ModelViewSet):
    queryset = Patient.objects.all()
    serializer_class = PatientSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    @action(methods=['GET'], detail=False)
    def search(self, request):
        try:
            region = request.data['region']
            search = request.data['search']
            query = self.queryset.filter(Q(region_id=region) | Q(name=search))
            ser = self.serializer_class(query)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class RetseptsView(viewsets.ModelViewSet):
    queryset = Retsepts.objects.all()
    serializer_class = RetseptsSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    @action(methods=['GET'], detail=False)
    def get_retsepts_of_doctor(self, request):
        try:
            user = request.user
            query = self.queryset.filter(doctor=user)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})

    @action(methods=['GET'], detail=False)
    def get_retsepts_of_client(self, request):
        try:
            client = request.GET['client']
            query = self.queryset.filter(client=client)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class CommentsView(viewsets.ModelViewSet):
    queryset = Comments.objects.all()
    serializer_class = CommentsSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    def create(self, request):
        try:
            patient_id = request.data['patient']
            doctor_id = request.data['doctor']
            ill = request.data['ill']
            com = Comments.objects.create(patient_id=patient_id, doctor_id=doctor_id, ill=ill)
            ser = self.serializer_class(com)
            return Response(ser.data)

        except Exception as err:
            return Response({'error': f'{err}'})

    @action(methods=['GET'], detail=False)
    def get_comment_of_client(self, request):
        try:
            client = request.GET['client']
            query = self.queryset.filter(client=client)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})

    @action(methods=['GET'], detail=False)
    def get_comment_of_doctor(self, request):
        try:
            user = request.user
            query = self.queryset.filter(doctor=user)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class PharmacyView(viewsets.ModelViewSet):
    queryset = Pharmacy.objects.all()
    serializer_class = PharmacySerializer
    authentication_classes = (MyOwnTokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    @action(methods=['GET'], detail=False)
    def get_retsepts(self, request):
        try:
            patient_id = request.GET['patient']
            ret = Retsepts.objects.filter(patient_id=patient_id, active=True).last()
            ser = RetseptsSerializer(ret)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})

    @action(methods=['GET'], detail=False)
    def search_client(self, request):
        try:
            region = int(request.GET['region'])
            search = request.GET['search']
            ret = Patient.objects.filter(region_id=region, name__icontains=search)
            ser = PatientSerializer(ret, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})
