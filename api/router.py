from rest_framework import routers
from .views import *
router = routers.DefaultRouter()

router.register('doctor', DoctorView)
router.register('direction', DirectionView)
router.register('region', RegionView)
router.register('hospital', HospitalView)
router.register('doctor', DoctorView)
router.register('patient', PatientView)
router.register('retsepts', RetseptsView)
router.register('comments', CommentsView)
router.register('pharmacy', PharmacyView)
